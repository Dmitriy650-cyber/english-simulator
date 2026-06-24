using System.Reflection;

namespace EnglishSimulator.Desktop.ViewModels
{
	public class SimulatorViewModel(
		IMessageBoxService messageBoxService,
		IAudioPlayerService audioPlayerService,
		INavigationService navigationService,
		ISentenceRepository sentenceRepository) : ViewModel(messageBoxService), ITransientDependency
	{
		private CancellationTokenSource? _cts;
		private Task? _task;
		private ManualResetEventSlim? _pauseEvent;
		private bool _reviseMode = false;

		private bool _isLastPage => Sentences.Count == 0;

		private List<Sentence> _currentSentences = [];
		private List<Sentence> _failedSentences = [];

		public List<Sentence> Sentences { get; set; } = [];

		#region Свойства

		/// <summary>
		/// Текущая колода
		/// </summary>
		public Deck? Deck
		{
			get => field;
			set => Set(ref field, value);
		}

		#region Предложения

		public string? Text1
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton1Selected
		{
			get => field;
			set => Set(ref field, value);
		}

		public string? Text2
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton2Selected
		{
			get => field;
			set => Set(ref field, value);
		}

		public string? Text3
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton3Selected
		{
			get => field;
			set => Set(ref field, value);
		}

		public string? Text4
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton4Selected
		{
			get => field;
			set => Set(ref field, value);
		}
		public string? Text5
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton5Selected
		{
			get => field;
			set => Set(ref field, value);
		}

		public string? Text6
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton6Selected
		{
			get => field;
			set => Set(ref field, value);
		}

		public string? Text7
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton7Selected
		{
			get => field;
			set => Set(ref field, value);
		}

		public string? Text8
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton8Selected
		{
			get => field;
			set => Set(ref field, value);
		}
		public string? Text9
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton9Selected
		{
			get => field;
			set => Set(ref field, value);
		}
		public string? Text10
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton10Selected
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton1IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton2IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton3IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton4IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton5IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton6IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton7IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton8IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton9IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool ToggleButton10IsEnabled
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		public string? ButtonStartText
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool IsChooseMode
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool IsPause
		{
			get => field;
			set => Set(ref field, value);
		}

		public int Speed
		{
			get => field;
			set => Set(ref field, value);
		}

		public string? TextInfo
		{
			get => field;
			set => Set(ref field, value);
		}

		public bool IsLessonStarted
		{
			get => field;
			set => Set(ref field, value);
		}

		#region Свойства для подсчета количества предложений

		public int CountLearnedSentences
		{
			get => field;
			set => Set(ref field, value);
		}

		public int CountNewSentences
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		#endregion

		public override async Task InitializeViewModelAsync()
		{
			Caption = "SIMULATOR";
			Deck = (Deck)InputData!;
			ButtonStartText = "Start";
			var sentences = Deck.Sentences.Where(n => n.RepeatDate < DateTime.Now).ToList();
			if (sentences.Count == 0)
			{
				sentences = Deck.Sentences;
				_reviseMode = true;
			}
			Sentences.AddRange(EnumerableExtensions.Shuffle(sentences));
			CountNewSentences = Sentences.Count;
			Speed = 0;
		}

		#region Команды

		/// <summary>
		/// Перейти на страницу помощи
		/// </summary>
		public ICommand? GoToHelpPageCommand => new LambdaCommand(() =>
		{
			_cts?.Cancel();
			navigationService.NavigateTo(nameof(HelpPage), null!);
		});

		/// <summary>
		/// Команда начала урока / паузы урока / продолжения урока
		/// </summary>
		public ICommand? StartCommand => new LambdaCommand(async () =>
		{
			if (IsLessonStarted)
			{
				if (IsChooseMode)
				{
					// Продолжение урока
					await TestKnowledgeAsync();
					if (_isLastPage)
					{
						// Завершение урока
						_cts!.Cancel();
						navigationService.NavigateTo(nameof(CongratulationsPage), null!);
					}
					ShuffleSentences();
					ResetAllToggleButtons();
					IsChooseMode = false;
					_pauseEvent?.Set();
				}
				else
				{
					if (IsPause)
					{
						// Снятие паузы
						IsPause = false;
						ButtonStartText = "Pause";
						_pauseEvent?.Set();
					}
					else
					{
						// Поставить на паузу
						IsPause = true;
						ButtonStartText = "Reset";
						_pauseEvent?.Reset();
					}
				}
			}
			else
			{
				_cts = new CancellationTokenSource();
				_pauseEvent = new ManualResetEventSlim(true);
				IsLessonStarted = true;
				_task = Task.Run(() => StartLessonAsync(_cts!.Token), _cts!.Token);
			}
		});

		/// <summary>
		/// Вернуться назад
		/// </summary>
		public ICommand? BackCommand => new LambdaCommand(() =>
		{
			_cts?.Cancel();
			navigationService.NavigateTo(nameof(DeckPage), null!);
		});

		#endregion

		/// <summary>
		/// Начать урок
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		private async Task StartLessonAsync(CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				try
				{
					ButtonStartText = "Pause";
					SetCurrentSentences();

					token.ThrowIfCancellationRequested();
					_pauseEvent!.Wait(token);

					for (int i = 0; i < _currentSentences.Count; i++)
					{
						TextInfo = "Learning...";

						var button = GetSentenceProperty(i + 1);
						var isButtonSelected = GetToggleButtonIsSelectedProperty(i + 1);
						var sentence = _currentSentences[i];

						isButtonSelected!.SetValue(this, true);

						await Task.Delay(Speed * 500, token);
						token.ThrowIfCancellationRequested();
						_pauseEvent!.Wait(token);

						// Читаем на русском
						await audioPlayerService.PlayWavFileAsync(GetAudioFileFullPath(sentence.RussianAudio), token);
						token.ThrowIfCancellationRequested();
						_pauseEvent!.Wait(token);

						await Task.Delay(Speed * 500, token);
						token.ThrowIfCancellationRequested();
						_pauseEvent!.Wait(token);

						// Устанавливает английский текст и читаем на английском
						await Application.Current.Dispatcher.InvokeAsync(async () =>
						{
							button!.SetValue(this, sentence.EnglishText);
						});
						await audioPlayerService.PlayWavFileAsync(GetAudioFileFullPath(sentence.EnglishAudio), token);
						token.ThrowIfCancellationRequested();
						_pauseEvent!.Wait(token);

						await Task.Delay(Speed * 500, token);
						token.ThrowIfCancellationRequested();
						_pauseEvent!.Wait(token);

						// Читаем на английском
						await audioPlayerService.PlayWavFileAsync(GetAudioFileFullPath(sentence.EnglishAudio), token);
						token.ThrowIfCancellationRequested();
						_pauseEvent!.Wait(token);

						isButtonSelected.SetValue(this, false);

						CountLearnedSentences++;
					}

					for (int i = 0; i < _currentSentences.Count; i++)
					{
						TextInfo = "Repetition...";

						var button = GetSentenceProperty(i + 1);
						var isButtonSelected = GetToggleButtonIsSelectedProperty(i + 1);
						var sentence = _currentSentences[i];

						isButtonSelected!.SetValue(this, true);

						await Task.Delay(Speed * 500, token);
						token.ThrowIfCancellationRequested();
						_pauseEvent!.Wait(token);

						// Читаем на английском
						await audioPlayerService.PlayWavFileAsync(GetAudioFileFullPath(sentence.EnglishAudio), token);
						token.ThrowIfCancellationRequested();
						_pauseEvent!.Wait(token);

						isButtonSelected.SetValue(this, false);
					}

					SetToggleButtonsEnabled();
					IsChooseMode = true;
					TextInfo = "Select the sentences where you made mistakes";

					ButtonStartText = "Continue";

					_pauseEvent!.Reset();
					_pauseEvent!.Wait(token);
				}
				catch (OperationCanceledException)
				{
					StopTask();
				}
				catch (Exception ex)
				{
					MessageBoxService.Error(ex.Message);
				}
			}
		}

		/// <summary>
		/// Узнать, какие предложения пользователь знает, а какие нет. Составить дальнейшую программу
		/// </summary>
		private async Task TestKnowledgeAsync()
		{
			(var learnedSentences, var dontLearnedSentences) = GetStudyResult();

			if (!_reviseMode)
			{
				await MakeRepositoryRequestAsync(async () =>
				{
					foreach (var sentence in learnedSentences)
					{
						if (_failedSentences.Contains(sentence))
							return;

						var response = await sentenceRepository.SetNextStageAsync(sentence.Id);

						if (response.IsFail)
						{
							MessageBoxService.Error(response.ErrorMessage);
							return;
						}
					}

					foreach (var sentence in dontLearnedSentences)
					{
						if (_failedSentences.Contains(sentence))
							return;

						var response = await sentenceRepository.ResetStageAsync(sentence.Id, Deck!.Id);

						if (response.IsFail)
						{
							MessageBoxService.Error(response.ErrorMessage);
							return;
						}

						_failedSentences.Add(sentence);
					}
				});
			}

			foreach(var sentence in learnedSentences)
			{
				Sentences.Remove(sentence);
			}
			foreach(var sentence in dontLearnedSentences)
			{
				Sentences.Add(sentence);
			}

			CountLearnedSentences -= dontLearnedSentences.Count;
		}

		/// <summary>
		/// Получить выученные и требующие повторения предложения
		/// </summary>
		/// <returns></returns>
		private (List<Sentence> learnedSentences, List<Sentence> dontLearnedSentences) GetStudyResult()
		{
			var learnedSentences = new List<Sentence>();
			var dontLearnedSentences = new List<Sentence>();

			for (int i = 0; i < _currentSentences.Count; i++)
			{
				var state = GetSentenceSelectedState(i + 1);
				if (state)
					dontLearnedSentences.Add(_currentSentences[i]);
				else
					learnedSentences.Add(_currentSentences[i]);
			}

			return (learnedSentences, dontLearnedSentences);
		}

		/// <summary>
		/// Узнать, выделена кнопка или нет
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		private bool GetSentenceSelectedState(int number)
		{
			var button = GetToggleButtonIsSelectedProperty(number);
			return (bool)button!.GetValue(this)!;
		}

		/// <summary>
		/// Перемешать оставшиеся предложения
		/// </summary>
		private void ShuffleSentences()
		{
			var sentences = new List<Sentence>();
			sentences.AddRange(Sentences);
			if (Sentences.Count > 0)
				Sentences.Clear();

			Sentences.AddRange(EnumerableExtensions.Shuffle(sentences));
		}

		private void SetCurrentSentences()
		{
			_currentSentences = Sentences.Take(10).ToList();

			foreach (var sentences in _currentSentences)
				Sentences.Remove(sentences);
		}

		/// <summary>
		/// Получить полный путь к аудиофалу
		/// </summary>
		/// <param name="audioFileName"></param>
		/// <returns></returns>
		private string GetAudioFileFullPath(string audioFileName) =>
			Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AudioFiles", audioFileName);

		/// <summary>
		/// Получить предложение по номеру
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		private PropertyInfo? GetSentenceProperty(int number)
		{
			var propertyName = number switch
			{
				1 => nameof(Text1),
				2 => nameof(Text2),
				3 => nameof(Text3),
				4 => nameof(Text4),
				5 => nameof(Text5),
				6 => nameof(Text6),
				7 => nameof(Text7),
				8 => nameof(Text8),
				9 => nameof(Text9),
				10 => nameof(Text10),
				_ => null
			};

			return this.GetType().GetProperty(propertyName!);
		}

		/// <summary>
		/// Получить свойство для управления нажатием кнопки по ее номеру
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		private PropertyInfo? GetToggleButtonIsSelectedProperty(int number)
		{
			var propertyName = number switch
			{
				1 => nameof(ToggleButton1Selected),
				2 => nameof(ToggleButton2Selected),
				3 => nameof(ToggleButton3Selected),
				4 => nameof(ToggleButton4Selected),
				5 => nameof(ToggleButton5Selected),
				6 => nameof(ToggleButton6Selected),
				7 => nameof(ToggleButton7Selected),
				8 => nameof(ToggleButton8Selected),
				9 => nameof(ToggleButton9Selected),
				10 => nameof(ToggleButton10Selected),
				_ => null
			};

			return this.GetType().GetProperty(propertyName!);
		}


		/// <summary>
		/// Получить свойство для управления активностью кноки по ее номеру
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		private PropertyInfo? GetToggleButtonIsEnabledProperty(int number)
		{
			var propertyName = number switch
			{
				1 => nameof(ToggleButton1IsEnabled),
				2 => nameof(ToggleButton2IsEnabled),
				3 => nameof(ToggleButton3IsEnabled),
				4 => nameof(ToggleButton4IsEnabled),
				5 => nameof(ToggleButton5IsEnabled),
				6 => nameof(ToggleButton6IsEnabled),
				7 => nameof(ToggleButton7IsEnabled),
				8 => nameof(ToggleButton8IsEnabled),
				9 => nameof(ToggleButton9IsEnabled),
				10 => nameof(ToggleButton10IsEnabled),
				_ => null
			};

			return this.GetType().GetProperty(propertyName!);
		}

		/// <summary>
		/// Активировать все кнопки, в которых есть текст
		/// </summary>
		private void SetToggleButtonsEnabled()
		{
			for (int i = 0; i < _currentSentences.Count(); i++)
			{
				GetToggleButtonIsEnabledProperty(i + 1)!.SetValue(this, true);
			}
		}

		/// <summary>
		/// Очистить весь текст на кнопках, снять выделение и заблокировать нажатия
		/// </summary>
		private void ResetAllToggleButtons()
		{
			Text1 = null;
			ToggleButton1Selected = false;
			ToggleButton1IsEnabled = false;

			Text2 = null;
			ToggleButton2Selected = false;
			ToggleButton2IsEnabled = false;

			Text3 = null;
			ToggleButton3Selected = false;
			ToggleButton3IsEnabled = false;

			Text4 = null;
			ToggleButton4Selected = false;
			ToggleButton4IsEnabled = false;

			Text5 = null;
			ToggleButton5Selected = false;
			ToggleButton5IsEnabled = false;

			Text6 = null;
			ToggleButton6Selected = false;
			ToggleButton6IsEnabled = false;

			Text7 = null;
			ToggleButton7Selected = false;
			ToggleButton7IsEnabled = false;

			Text8 = null;
			ToggleButton8Selected = false;
			ToggleButton8IsEnabled = false;

			Text9 = null;
			ToggleButton9Selected = false;
			ToggleButton9IsEnabled = false;

			Text10 = null;
			ToggleButton10Selected = false;
			ToggleButton10IsEnabled = false;
		}

		private void StopTask()
		{
			_cts?.Dispose();
			_pauseEvent?.Dispose();
			_cts = null;
			_pauseEvent = null;
			_task = null;
		}
	}
}
