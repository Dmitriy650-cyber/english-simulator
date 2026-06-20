namespace EnglishSimulator.Desktop.Repositories.Interfaces
{
	public interface IDeckRepository
	{
		Task<RepositoryResponse<Deck>> CreateOrUpdateDeckAsync(Deck model);
		Task<RepositoryResponse> DeleteDeckAsync(int id);
		Task<RepositoryResponse<Deck[]>> GetDecksAsync();
		Task<RepositoryResponse<Deck?>> GetDeckByIdAsync(int id);
	}
}