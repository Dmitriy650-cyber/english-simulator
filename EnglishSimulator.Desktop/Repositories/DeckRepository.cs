using EnglishSimulator.Desktop.Data;
using EnglishSimulator.Desktop.Models.RepositoryResponses;

namespace EnglishSimulator.Desktop.Repositories
{
    public class DeckRepository(DataContext context)
    {
        public async Task<RepositoryResponse<Deck>> CreateDeckAsync(Deck deck)
        {
			try
			{
				if (string.IsNullOrWhiteSpace(deck.Name))
					return RepositoryResponse<Deck>.Fail("Deck name is required");
				if (deck.Id != 0)
					return RepositoryResponse<Deck>.Fail("Deck id should be zero");
				if (deck.CountSentensesPerLesson < 10 || deck.CountSentensesPerLesson > 1000)
					return RepositoryResponse<Deck>.Fail("Deck count sentences per lesson should be less than 1000 and more than 10");
				if (deck.CountSentensesPerLesson == 0)
					deck.CountSentensesPerLesson = DataContextConstants.CountSentencesPerLesson;

				var newDeck = context.Decks.Add(deck).Entity;
				await context.SaveChangesAsync();

				return RepositoryResponse<Deck>.Success(newDeck);
			}
			catch (Exception ex)
			{
				return RepositoryResponse<Deck>.Fail(ex.Message);
			}
        }
    }
}
