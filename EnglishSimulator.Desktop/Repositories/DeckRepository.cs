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
				if (!Deck.IsValid(deck, out string message))
					return RepositoryResponse<Deck>.Fail(message);
				if (deck.Id != 0)
					return RepositoryResponse<Deck>.Fail("Deck id should be zero");
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
