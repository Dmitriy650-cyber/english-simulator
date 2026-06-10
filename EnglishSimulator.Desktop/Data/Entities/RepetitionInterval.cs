namespace EnglishSimulator.Desktop.Data.Entities
{
    public class RepetitionInterval
    {
        public int Id { get; set; }
        public int Stage { get; set; }
        public int CountDays { get; set; }

        public virtual Deck? Deck { get; set; }
        public int DeckId { get; set; }

        public static bool IsValid(RepetitionInterval repetitionInterval, out string message)
        {
            if (repetitionInterval.Stage < 0 || repetitionInterval.Stage > DataContextConstants.CountRepetitionIntervalsInDeck)
            {
                message = $"Repetition interval stage should be equal zero and less than {DataContextConstants.CountRepetitionIntervalsInDeck}";
                return false;
            }

            if (repetitionInterval.CountDays < 1 || repetitionInterval.CountDays > 360)
            {
                message = "Repetition interval count days should be more than 0 and less than 360";
                return false;
            }

            if (repetitionInterval.DeckId < 1 || repetitionInterval.DeckId > int.MaxValue)
            {
                message = $"Repetition interval deckId should be more than 0 and less than {int.MaxValue}";
                return false;
            }

            message = "Success";
            return true;
        }
    }
}
