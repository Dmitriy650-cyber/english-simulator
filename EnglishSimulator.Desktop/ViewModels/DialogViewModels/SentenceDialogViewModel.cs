namespace EnglishSimulator.Desktop.ViewModels.DialogViewModels
{
    public class SentenceDialogViewModel : DialogViewModelBase, ITransientDependency
    {
        private readonly IDialogService? _dialogService;

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

		public SentenceDialogViewModel(Sentence? sentence, IDialogService dialogService)
		{
			if (sentence is { })
            {
                EnglishText = sentence.EnglishText;
                EnglishAudio = sentence.EnglishAudio;
                RussianText = sentence.RussianText;
                RussianAudio = sentence.RussianAudio;
            }
            _dialogService = dialogService;
		}

		public SentenceDialogViewModel()
		{
			
		}

        #region Команды

        /// <summary>
        /// Создать файл с русским аудио
        /// </summary>
        public ICommand? CreateRussianAudioCommand => new LambdaCommand(async () =>
        { 
            var result = await _dialogService!.ShowRecoderDialogWindow();

            if (result is not null)
            {
				if (!string.IsNullOrEmpty(RussianAudio))
					DeleteAudioFile(RussianAudio);

				RussianAudio = SaveAudioFile(result.AudioBuffer.ToArray(), result.WaveFormat);
            }
        });

        /// <summary>
        /// Сохранить файл с английским аудио
        /// </summary>
        public ICommand? CreateEnglishAudioCommand => new LambdaCommand(async () =>
        {
            var result = await _dialogService!.ShowRecoderDialogWindow();

            if (result is not null)
            {
				if (!string.IsNullOrEmpty(EnglishAudio))
					DeleteAudioFile(EnglishAudio);

                EnglishAudio = SaveAudioFile(result.AudioBuffer.ToArray(), result.WaveFormat);
			}
        });

        /// <summary>
        /// Отменить действия и закрыть окно
        /// </summary>
        public ICommand? NewCancelCommand => new LambdaCommand(() =>
        {
            if (EnglishAudio != null)
                DeleteAudioFile(EnglishAudio);
            if (RussianAudio != null)
                DeleteAudioFile(RussianAudio);

            CancelCommand?.Execute(null);
        });

        /// <summary>
        /// Сохранить все и закрыть окно
        /// </summary>
        public ICommand NewOkCommand => new LambdaCommand(async () =>
        {
            OkCommand?.Execute(null);
        }, () =>
        {
            if (string.IsNullOrWhiteSpace(RussianAudio)
            || string.IsNullOrWhiteSpace(EnglishAudio)
            || string.IsNullOrWhiteSpace(RussianText)
            || string.IsNullOrWhiteSpace(EnglishText))
                return false;

            return true;
        });

        #endregion

        /// <summary>
        /// Сохранить аудио файл
        /// </summary>
        /// <param name="audioData"></param>
        /// <param name="waveFormat"></param>
        private string SaveAudioFile(byte[] audioData, WaveFormat waveFormat)
        {
            string recordingFolder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "AudioFiles");

            Directory.CreateDirectory(recordingFolder);

            string filePath = Path.Combine(
                recordingFolder,
                $"recording_{DateTime.Now:yyyyMMdd_HHmmss}.wav");

            using var writer = new WaveFileWriter(filePath, waveFormat);
            writer.Write(audioData, 0, audioData.Length);

            return filePath;
        }

        /// <summary>
        /// Удалить аудио файл
        /// </summary>
        /// <param name="filePath"></param>
        private void DeleteAudioFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
