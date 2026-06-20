namespace EnglishSimulator.Desktop.ViewModels.DialogViewModels
{
	public class RecorderDialogViewModel : DialogViewModelBase, ITransientDependency
	{
		private WaveInEvent? _waveIn;
		private Timer? _timer;
		private TimeSpan _timeSpan;

		public WaveFormat? WaveFormat;
		public List<byte> AudioBuffer = [];

		#region Свойства

		/// <summary>
		/// Идет ли запись?
		/// </summary>
		public bool IsRecording
		{
			get => field;
			set => Set(ref field, value);
		}

		/// <summary>
		/// Время записи в виде строки
		/// </summary>
		public string? Time
		{
			get => field;
			set => Set(ref field, value);
		}

		/// <summary>
		/// Нажата ли кнопка "Начать запись"?
		/// </summary>
		public bool IsButtonStartChecked
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		#region Команды

		public ICommand? StartCommand => new LambdaCommand(() =>
		{
			if (IsRecording == false)
			{
				StartRecording();
			}
			else
			{
				StopRecording();
			}
		});

		public ICommand? SaveCommand => new LambdaCommand(() =>
		{
			SaveRecording();
			OkCommand?.Execute(null!);
		}, () => IsRecording == true || AudioBuffer.Count != 0);

		public ICommand? CurrentCancelCommand => new LambdaCommand(() =>
		{
			CancelRecording();
			CancelCommand?.Execute(null!);
		});

		#endregion

		public RecorderDialogViewModel()
		{
			StartRecording();
		}

		public void StartRecording()
		{
			if (IsRecording) return;

			if (AudioBuffer.Count > 0)
				AudioBuffer.Clear();

			_timeSpan = new TimeSpan();
			Time = _timeSpan.ToString(@"mm\:ss\:fff");

			_waveIn = new WaveInEvent()
			{
				WaveFormat = new WaveFormat(44100, 16, 1),
			};

			WaveFormat = _waveIn.WaveFormat;

			_waveIn.DataAvailable += (sender, e) =>
			{
				AudioBuffer.AddRange(e.Buffer.Take(e.BytesRecorded));
			};

			_waveIn.RecordingStopped += OnRecordingStopped;

			_waveIn.StartRecording();
			IsButtonStartChecked = true;
			_timer = new(TimerTick, null, 10, 10);
			IsRecording = true;
		}

		private void OnRecordingStopped(object? sender, StoppedEventArgs e)
		{
			IsRecording = false;
		}

		public void StopRecording()
		{
			if (!IsRecording) return;

			IsButtonStartChecked = false;
			_waveIn?.StopRecording();
			_timer?.Dispose();
			_waveIn?.Dispose();
			_waveIn = null;
		}

		public void SaveRecording()
		{
			StopRecording();

			if (AudioBuffer.Count == 0) return;
		}

		public void CancelRecording()
		{
			IsButtonStartChecked = false;
			AudioBuffer.Clear();
			IsRecording = false;
			_timer?.Dispose();
			_waveIn?.Dispose();
			_waveIn = null;
		}

		private void TimerTick(object? state)
		{
			_timeSpan = _timeSpan.Add(TimeSpan.FromMilliseconds(10));
			Time = _timeSpan.ToString(@"mm\:ss\:fff");
		}
	}
}
