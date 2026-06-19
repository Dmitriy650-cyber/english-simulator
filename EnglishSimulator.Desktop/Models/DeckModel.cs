namespace EnglishSimulator.Desktop.Models
{
    public class DeckModel : ViewModelBase
    {
        #region Свойства

        /// <summary>
        /// Колода
        /// </summary>
        public Deck? Deck
        {
            get => field;
            set => Set(ref field, value);
        }

        /// <summary>
        /// Количество новый предложений
        /// </summary>
        public int CountNewSentences
        {
            get => field;
            set => Set(ref field, value);
        }

        /// <summary>
        /// Количество изучаемых предложений
        /// </summary>
        public int CountLearnSentences
        {
            get => field;
            set => Set(ref field, value);
        }

        /// <summary>
        /// Количество предложений для повторения
        /// </summary>
        public int CountDueSentences
        {
            get => field;
            set => Set(ref field, value);
        }

        #endregion
    }
}
