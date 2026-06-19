namespace EnglishSimulator.Desktop.Data.Entities
{
    public class Sentence
    {
        public int Id { get; set; }
        public string RussianText { get; set; } = null!;
        public string EnglishText { get; set; } = null!;
        public string RussianAudio { get; set; } = null!;
        public string EnglishAudio { get; set; } = null!;
        public string State { get; set; } = null!;
        public int Stage { get; set; }
        public DateTime RepeatDate { get; set; }

        public virtual Deck? Deck { get; set; }
        public int DeckId { get; set; }

        public static bool IsValid(Sentence sentence, out string message)
        {
            if (string.IsNullOrWhiteSpace(sentence.RussianText))
            {
                message = "Russian text is required";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sentence.EnglishText))
            {
                message = "English text is required";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sentence.RussianAudio))
            {
                message = "Russian audio is required";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sentence.EnglishAudio))
            {
                message = "English audio is required";
                return false;
            }

			if (sentence.DeckId < 1 || sentence.DeckId > int.MaxValue)
			{
				message = $"DeckId should be more than 0 and less than {int.MaxValue}";
				return false;
			}

            if (sentence.RepeatDate <= DateTime.UtcNow)
            {
                message = "RepeatDate should be more than UtcNow";
                return false;
            }

			message = "Success";
            return true;
        }
    }
}
