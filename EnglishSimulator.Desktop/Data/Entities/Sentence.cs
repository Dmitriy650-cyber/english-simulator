namespace EnglishSimulator.Desktop.Data.Entities
{
    public class Sentence
    {
        public int Id { get; set; }
        public string RussianText { get; set; } = null!;
        public string EnglishText { get; set; } = null!;
        public string? RussianAudio { get; set; }
        public string? EnglishAudio { get; set; }
        public string State { get; set; } = null!;
        public int Stage { get; set; }
        public DateTime RepeatDate { get; set; }

        public virtual Deck? Deck { get; set; }
        public int DeckId { get; set; }
    }
}
