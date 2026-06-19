namespace EnglishSimulator.Desktop.ViewModels.DialogViewModels
{
    public class InputDialogViewModel : DialogViewModelBase, ITransientDependency
    {
        #region Свойства

        /// <summary>
        /// Заголовок
        /// </summary>
        public string? Title
        {
            get => field;
            set => Set(ref field, value);
        }

        /// <summary>
        /// Введенный текст
        /// </summary>
        public string? InputText
        {
            get => field;
            set => Set(ref field, value);
        }

		#endregion

		public InputDialogViewModel(string title)
		{
            Title = title;
		}

		public InputDialogViewModel()
		{
			
		}
	}
}
