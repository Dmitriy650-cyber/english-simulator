namespace EnglishSimulator.Desktop.Repositories
{
	public class RepetitionIntervalRepository(DataContext context) : IRepetitionIntervalRepository, ISingletonDependency
	{
		public async Task<RepositoryResponse<RepetitionInterval[]>> GetRepetitionIntervalsByDeckId(int id) =>
			RepositoryResponse<RepetitionInterval[]>.Success(await context.RepetitionIntervals
				.AsNoTracking()
				.Where(n => n.DeckId == id)
				.OrderBy(n => n.Stage)
				.ToArrayAsync()
				.ConfigureAwait(false));

		public async Task<RepositoryResponse<RepetitionInterval>> UpdateRepetitionIntervalAsync(RepetitionInterval model)
		{
			try
			{
				if (!RepetitionInterval.IsValid(model, out string message))
					return RepositoryResponse<RepetitionInterval>.Fail(message);

				var repetitionInterval = await context.RepetitionIntervals.FindAsync(model.Id).ConfigureAwait(false);
				if (repetitionInterval is null)
					return RepositoryResponse<RepetitionInterval>.Fail("Repetition interval not found");

				repetitionInterval.CountDays = model.CountDays;

				await context.SaveChangesAsync().ConfigureAwait(false);

				return RepositoryResponse<RepetitionInterval>.Success(repetitionInterval);
			}
			catch (Exception ex)
			{
				return RepositoryResponse<RepetitionInterval>.Fail(ex.Message);
			}
		}
	}
}
