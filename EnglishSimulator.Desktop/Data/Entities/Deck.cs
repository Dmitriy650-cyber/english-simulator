namespace EnglishSimulator.Desktop.Data.Entities
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountSentensesPerLesson { get; set; }

        public virtual List<Sentence> Sentences { get; set; } = [];
        public virtual List<RepetitionInterval> RepetitionIntervals { get; set; } = [];
	}
}
