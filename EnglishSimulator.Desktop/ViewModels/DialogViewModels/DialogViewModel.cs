namespace EnglishSimulator.Desktop.ViewModels.DialogViewModels
{
    public class DialogViewModel : DialogViewModelBase, ITransientDependency
    {
        #region Свойства

        public string? Question
        {
            get => field;
            set => Set(ref field, value);
        }

		#endregion

		public DialogViewModel(string question)
		{
            Question = question;
		}

		public DialogViewModel()
		{
			
		}
	}
}
