namespace EnglishSimulator.Desktop.Repositories
{
	public class SentenceRepository(DataContext context) : ISentenceRepository, ISingletonDependency
	{
		public async Task<RepositoryResponse<Sentence[]>> GetSentencesByDeckId(int id) =>
			RepositoryResponse<Sentence[]>.Success(await context.Sentences
				.AsNoTracking()
				.Where(n => n.DeckId == id)
				.ToArrayAsync()
				.ConfigureAwait(false));

		public async Task<RepositoryResponse<Sentence>> CreateOrUpdateSentenceAsync(Sentence model)
		{
			try
			{
				if (!Sentence.IsValid(model, out string message))
					return RepositoryResponse<Sentence>.Fail(message);

				Sentence? sentence = new();

				if (model.Id == 0)
				{
					model.Stage = 0;
					model.State = nameof(SentenceState.New);
					model.RepeatDate = DateTime.Now.AddDays(1).Date;
					sentence = context.Sentences.Add(model).Entity;
				}
				else
				{
					sentence = await context.Sentences.FindAsync(model.Id).ConfigureAwait(false);
					if (sentence is null)
						return RepositoryResponse<Sentence>.Fail("Sentence not found");

					sentence.RussianText = model.RussianText;
					sentence.EnglishText = model.EnglishText;
					sentence.RussianAudio = model.RussianAudio;
					sentence.EnglishAudio = model.EnglishAudio;
				}

				await context.SaveChangesAsync().ConfigureAwait(false);

				return RepositoryResponse<Sentence>.Success(sentence);
			}
			catch (Exception ex)
			{
				return RepositoryResponse<Sentence>.Fail(ex.Message);
			}
		}

		public async Task<RepositoryResponse> DeleteSentenceAsync(int id)
		{
			try
			{
				var sentence = await context.Sentences.FindAsync(id).ConfigureAwait(false);
				if (sentence is null)
					return RepositoryResponse.Fail("Sentence not found");

				var russianAudio = sentence.RussianAudio;
				var englishAudio = sentence.EnglishAudio;

				context.Sentences.Remove(sentence);
				await context.SaveChangesAsync().ConfigureAwait(false);

				FileService.DeleteAudioFile(russianAudio);
				FileService.DeleteAudioFile(englishAudio);

				return RepositoryResponse.Success();
			}
			catch (Exception ex)
			{
				return RepositoryResponse.Fail(ex.Message);
			}
		}

		public async Task<RepositoryResponse> SetNextStageAsync(int id)
		{
			try
			{
				var sentence = await context.Sentences.FindAsync(id).ConfigureAwait(false);
				if (sentence is null)
					return RepositoryResponse.Fail("Sentence not found");

				if (sentence.Stage < DataContextConstants.CountRepetitionIntervalsInDeck)
					sentence.Stage++;

				if (sentence.Stage == DataContextConstants.AtWhatStageIsTheSentenceConsideredToBeLearnt)
					sentence.State = nameof(SentenceState.Learn);
				if (sentence.Stage == DataContextConstants.AtWhatStageIsTheSentenceConsideredToBeDue)
					sentence.State = nameof(SentenceState.Due);

				var repetitionInterval = await context.RepetitionIntervals
					.AsNoTracking()
					.FirstAsync(n => n.DeckId == sentence.DeckId && n.Stage == sentence.Stage)
					.ConfigureAwait(false);

				sentence.RepeatDate = DateTime.Now.AddDays(repetitionInterval.CountDays).Date;

				await context.SaveChangesAsync().ConfigureAwait(false);

				return RepositoryResponse.Success();
			}
			catch (Exception ex)
			{
				return RepositoryResponse.Fail(ex.Message);
			}
		}

		public async Task<RepositoryResponse> ResetStageAsync(int id, int deckId)
		{
			try
			{
				var sentence = await context.Sentences.FindAsync(id).ConfigureAwait(false);
				if (sentence is null)
					return RepositoryResponse.Fail("Sentence not found");

				if (!sentence.State.Equals(nameof(SentenceState.New)))
					sentence.State = nameof(SentenceState.Learn);

				var stateZero = await context.RepetitionIntervals
					.AsNoTracking()
					.FirstAsync(n => n.DeckId == deckId && n.Stage == 0);

				sentence.Stage = 0;
				sentence.RepeatDate = DateTime.Now.AddDays(stateZero.CountDays).Date;

				await context.SaveChangesAsync().ConfigureAwait(false);

				return RepositoryResponse.Success();
			}
			catch (Exception ex)
			{
				return RepositoryResponse.Fail(ex.Message);
			}
		}
	}
}
