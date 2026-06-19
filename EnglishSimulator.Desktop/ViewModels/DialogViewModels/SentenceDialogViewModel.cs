namespace EnglishSimulator.Desktop.ViewModels.DialogViewModels
{
    public class SentenceDialogViewModel : DialogViewModelBase
    {
        #region Свойства

        /// <summary>
        /// Английский текст
        /// </summary>
        public string? EnglishText
        {
            get => field;
            set => Set(ref field, value);
        }

        /// <summary>
        /// Английское аудио
        /// </summary>
        public string? EnglishAudio
        {
            get => field;
            set => Set(ref field, value);
        }

        /// <summary>
        /// Русский текст
        /// </summary>
        public string? RussianText
        {
            get => field;
            set => Set(ref field, value);
        }

        /// <summary>
        /// Русское аудио
        /// </summary>
        public string? RussianAudio
        {
            get => field;
            set => Set(ref field, value);
        }

		#endregion

		public SentenceDialogViewModel(Sentence? sentence)
		{
			if (sentence is { })
            {
                EnglishText = sentence.EnglishText;
                EnglishAudio = sentence.EnglishAudio;
                RussianText = sentence.RussianText;
                RussianAudio = sentence.RussianAudio;
            }
		}

		public SentenceDialogViewModel()
		{
			
		}
	}
}
