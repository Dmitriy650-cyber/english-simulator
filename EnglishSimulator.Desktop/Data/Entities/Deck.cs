namespace EnglishSimulator.Desktop.Data.Entities
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountSentensesPerLesson { get; set; }

        public virtual List<Sentence> Sentences { get; set; } = [];
        public virtual List<RepetitionInterval> RepetitionIntervals { get; set; } = [];

        public static bool IsValid(Deck deck, out string message)
        {
            if (string.IsNullOrWhiteSpace(deck.Name))
            {
                message = "Deck name is required";
                return false;
			}

            if (deck.CountSentensesPerLesson != 0 && (deck.CountSentensesPerLesson < 10 || deck.CountSentensesPerLesson > 1000))
            {
                message = "Deck count sentences per lesson should be less than 1000 and more than 10";
                return false;
			}

            message = "Success";
            return true;
        }
	}
}
