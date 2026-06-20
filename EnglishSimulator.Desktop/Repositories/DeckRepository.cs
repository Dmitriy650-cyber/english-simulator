namespace EnglishSimulator.Desktop.Repositories
{
	public class DeckRepository(DataContext context) : IDeckRepository, ISingletonDependency
	{
		public async Task<RepositoryResponse<Deck[]>> GetDecksAsync() =>
			RepositoryResponse<Deck[]>.Success(await context.Decks
				.Include(n => n.Sentences)
				.Include(n => n.RepetitionIntervals)
				.AsNoTracking()
				.ToArrayAsync()
				.ConfigureAwait(false));

		public async Task<RepositoryResponse<Deck?>> GetDeckByIdAsync(int id) =>
			RepositoryResponse<Deck?>.Success(await context.Decks
				.Include(n => n.Sentences)
				.Include(n => n.RepetitionIntervals)
				.FirstOrDefaultAsync(n => n.Id == id)
				.ConfigureAwait(false));

		public async Task<RepositoryResponse<Deck>> CreateOrUpdateDeckAsync(Deck model)
		{
			try
			{
				if (!Deck.IsValid(model, out string message))
					return RepositoryResponse<Deck>.Fail(message);
				if (model.CountSentensesPerLesson == 0)
					model.CountSentensesPerLesson = DataContextConstants.CountSentencesPerLesson;

				Deck? deck = new();
				var createMode = false;

				if (model.Id == 0)
				{
					deck = context.Decks.Add(model).Entity;
					createMode = true;
				}
				else
				{
					deck = await context.Decks.FindAsync(model.Id).ConfigureAwait(false);
					if (deck is null)
						return RepositoryResponse<Deck>.Fail("Deck not found");

					deck.Name = model.Name;
					deck.CountSentensesPerLesson = model.CountSentensesPerLesson;
				}

				await context.SaveChangesAsync().ConfigureAwait(false);

				if (createMode)
				{
					await context.RepetitionIntervals
						.AddRangeAsync(GetStandartRepetitionIntervals(deck.Id))
						.ConfigureAwait(false);
					await context.SaveChangesAsync().ConfigureAwait(false);
				}

				return RepositoryResponse<Deck>.Success(deck);
			}
			catch (Exception ex)
			{
				return RepositoryResponse<Deck>.Fail(ex.Message);
			}
		}

		public async Task<RepositoryResponse> DeleteDeckAsync(int id)
		{
			try
			{
				var deck = await context.Decks.FindAsync(id).ConfigureAwait(false);
				if (deck is null)
					return RepositoryResponse.Fail("Deck not found");

				context.Decks.Remove(deck);
				await context.SaveChangesAsync().ConfigureAwait(false);

				return RepositoryResponse.Success();
			}
			catch (Exception ex)
			{
				return RepositoryResponse.Fail(ex.Message);
			}
		}

		private RepetitionInterval[] GetStandartRepetitionIntervals(int deckId) => [
			new RepetitionInterval{
				Stage = 0,
				CountDays = 1,
				DeckId = deckId
			},
			new RepetitionInterval{
				Stage = 1,
				CountDays = 2,
				DeckId = deckId
			},
			new RepetitionInterval{
				Stage = 2,
				CountDays = 5,
				DeckId = deckId
			},
			new RepetitionInterval{
				Stage = 3,
				CountDays = 10,
				DeckId = deckId
			},
			new RepetitionInterval{
				Stage = 4,
				CountDays = 30,
				DeckId = deckId
			},
			new RepetitionInterval{
				Stage = 5,
				CountDays = 90,
				DeckId = deckId
			},
			];
	}
}
