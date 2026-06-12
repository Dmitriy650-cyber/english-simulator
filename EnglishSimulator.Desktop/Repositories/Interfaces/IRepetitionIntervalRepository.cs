namespace EnglishSimulator.Desktop.Repositories.Interfaces
{
	public interface IRepetitionIntervalRepository
	{
		Task<RepositoryResponse<RepetitionInterval[]>> GetRepetitionIntervalsByDeckId(int id);
		Task<RepositoryResponse<RepetitionInterval>> UpdateRepetitionIntervalAsync(RepetitionInterval model);
	}
}