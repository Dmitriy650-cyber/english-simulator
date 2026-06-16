namespace EnglishSimulator.Desktop.ViewModels.DialogViewModels
{
    public class AddDeckViewModel : DialogViewModelBase, ITransientDependency
    {
        #region Свойства

        /// <summary>
        /// Название колоды
        /// </summary>
        public string? DeckName
        {
            get => field;
            set => Set(ref field, value);
        }

        #endregion
    }
}
