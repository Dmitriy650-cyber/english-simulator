namespace EnglishSimulator.Desktop.ViewModels.DialogViewModels
{
    public class RepetitionIntervalsDialogViewModel : DialogViewModelBase, ITransientDependency
    {
        public List<RepetitionInterval> RepetitionIntervals = [];

        #region Свойства

        public string? Stage0
        {
            get => field;
            set => Set(ref field, value);
        }

        public string? Stage1
        {
            get => field;
            set => Set(ref field, value);
        }

        public string? Stage2
        {
            get => field;
            set => Set(ref field, value);
        }

        public string? Stage3
        {
            get => field;
            set => Set(ref field, value);
        }

        public string? Stage4
        {
            get => field;
            set => Set(ref field, value);
        }

        public string? Stage5
        {
            get => field;
            set => Set(ref field, value);
        }

		#endregion

		public RepetitionIntervalsDialogViewModel()
		{
			
		}

		public RepetitionIntervalsDialogViewModel(Deck deck)
		{
			Stage0 = deck.RepetitionIntervals.First(n => n.Stage == 0).CountDays.ToString();
			Stage1 = deck.RepetitionIntervals.First(n => n.Stage == 1).CountDays.ToString();
			Stage2 = deck.RepetitionIntervals.First(n => n.Stage == 2).CountDays.ToString();
			Stage3 = deck.RepetitionIntervals.First(n => n.Stage == 3).CountDays.ToString();
			Stage4 = deck.RepetitionIntervals.First(n => n.Stage == 4).CountDays.ToString();
			Stage5 = deck.RepetitionIntervals.First(n => n.Stage == 5).CountDays.ToString();

            RepetitionIntervals.AddRange(deck.RepetitionIntervals);
		}

        #region Команды

        /// <summary>
        /// Подтвердить изменения и закрыть окно
        /// </summary>
        public ICommand? NewOkCommand => new LambdaCommand(() =>
        {
            RepetitionIntervals.First(n => n.Stage == 0).CountDays = int.Parse(Stage0!);
            RepetitionIntervals.First(n => n.Stage == 1).CountDays = int.Parse(Stage1!);
            RepetitionIntervals.First(n => n.Stage == 2).CountDays = int.Parse(Stage2!);
            RepetitionIntervals.First(n => n.Stage == 3).CountDays = int.Parse(Stage3!);
            RepetitionIntervals.First(n => n.Stage == 4).CountDays = int.Parse(Stage4!);
            RepetitionIntervals.First(n => n.Stage == 5).CountDays = int.Parse(Stage5!);

            OkCommand?.Execute(null!);
        }, () =>
        {
            if (string.IsNullOrWhiteSpace(Stage0)
            || string.IsNullOrWhiteSpace(Stage1)
            || string.IsNullOrWhiteSpace(Stage2)
            || string.IsNullOrWhiteSpace(Stage3)
            || string.IsNullOrWhiteSpace(Stage4)
            || string.IsNullOrWhiteSpace(Stage5))
                return false;

            var result0 = int.TryParse(Stage0!, out int number0) && number0 > 0 && number0 < 1000;
            var result1 = int.TryParse(Stage1!, out int number1) && number1 > 0 && number1 < 1000;
            var result2 = int.TryParse(Stage2!, out int number2) && number2 > 0 && number2 < 1000;
            var result3 = int.TryParse(Stage3!, out int number3) && number3 > 0 && number3 < 1000;
            var result4 = int.TryParse(Stage4!, out int number4) && number4 > 0 && number4 < 1000;
            var result5 = int.TryParse(Stage5!, out int number5) && number5 > 0 && number5 < 1000;

            if (result0 && result1 && result2 && result3 && result4 && result5)
                return true;

            return false;
        });

        #endregion
    }
}
