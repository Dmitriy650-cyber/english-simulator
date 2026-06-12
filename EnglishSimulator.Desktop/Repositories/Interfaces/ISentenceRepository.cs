namespace EnglishSimulator.Desktop.Repositories.Interfaces
{
	public interface ISentenceRepository
	{
		Task<RepositoryResponse<Sentence>> CreateOrUpdateSentenceAsync(Sentence model);
		Task<RepositoryResponse> DeleteSentenceAsync(int id);
		Task<RepositoryResponse<Sentence[]>> GetSentencesByDeckId(int id);
		Task<RepositoryResponse> ResetStageAsync(int id);
		Task<RepositoryResponse> SetNextStageAsync(int id);
	}
}