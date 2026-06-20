namespace EnglishSimulator.Desktop.ViewModels
{
	public class EditViewModel(
		IMessageBoxService messageBoxService,
		INavigationService navigationService,
		IDialogService dialogService,
		ISentenceRepository sentenceRepository,
		IRepetitionIntervalRepository repetitionIntervalRepository,
		IDeckRepository deckRepository) : ViewModel(messageBoxService), ITransientDependency
	{
		public ObservableCollection<Sentence> Sentences { get; set; } = [];

		#region Свойства

		/// <summary>
		/// Текущая колода
		/// </summary>
		public Deck? Deck
		{
			get => field;
			set => Set(ref field, value);
		}
		
		/// <summary>
		/// Выбранное предложение
		/// </summary>
		public Sentence? SelectedSentence
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		public override async Task InitializeViewModelAsync()
		{
			Caption = "EDITOR";
			Deck = (Deck)InputData!;

			await GetNewSentencesAsync();
		}

		#region Команды

		/// <summary>
		/// Вернуться на страницу DeckPage
		/// </summary>
		public ICommand? GoBackCommand => new LambdaCommand(() =>
		{
			navigationService.NavigateTo(nameof(DeckPage), null!);
		});

		/// <summary>
		/// Добавить предложение
		/// </summary>
		public ICommand? AddSentenceCommand => new LambdaCommand(async () =>
		{
			var result = await dialogService.ShowSentenceDialogAsync(null);

			if (result is not null)
			{
				await MakeRepositoryRequestAsync(async () =>
				{
					result.DeckId = Deck!.Id;
					var response = await sentenceRepository.CreateOrUpdateSentenceAsync(result);

					if (response.IsFail)
					{
						MessageBoxService.Error(response.ErrorMessage);
						return;
					}

					await GetNewSentencesAsync();
				});
			}
		});

		/// <summary>
		/// Редактировать предложение
		/// </summary>
		public ICommand? EditSentenceCommand => new LambdaCommand(async () =>
		{
			var result = await dialogService.ShowSentenceDialogAsync(SelectedSentence!);

			if (result is not null)
			{
				await MakeRepositoryRequestAsync(async () =>
				{
					if (!SelectedSentence!.RussianAudio.Equals(result.RussianAudio))
						DeleteFile(SelectedSentence!.RussianAudio);
					if (!SelectedSentence!.EnglishAudio.Equals(result.EnglishAudio))
						DeleteFile(SelectedSentence!.EnglishAudio);

					SelectedSentence.RussianText = result.RussianText;
					SelectedSentence.EnglishText = result.EnglishText;
					SelectedSentence.RussianAudio = result.RussianAudio;
					SelectedSentence.EnglishAudio = result.EnglishAudio;

					var response = await sentenceRepository.CreateOrUpdateSentenceAsync(SelectedSentence);

					if (response.IsFail)
					{
						MessageBoxService.Error(response.ErrorMessage);
						return;
					}
					
					await GetNewSentencesAsync();
				});
			}
		}, () => SelectedSentence is not null);

		/// <summary>
		/// Удалить предложение
		/// </summary>
		public ICommand? DeleteSentenceCommand => new LambdaCommand(async () =>
		{
			var result = dialogService.ShowDialogAsync();

			if (result.Result == true)
			{
				await MakeRepositoryRequestAsync(async () =>
				{
					var response = await sentenceRepository.DeleteSentenceAsync(SelectedSentence!.Id);

					if (response.IsFail)
					{
						MessageBoxService.Error(response.ErrorMessage);
						return;
					}

					DeleteFile(SelectedSentence.RussianAudio);
					DeleteFile(SelectedSentence.EnglishAudio);
					await GetNewSentencesAsync();
				});
			}
		}, () => SelectedSentence is not null);

		/// <summary>
		/// Настроить временные интервалы
		/// </summary>
		public ICommand? SetUpTimeIntervalsCommand => new LambdaCommand(async () =>
		{
			var result = await dialogService.ShowRepetitionIntervalsDialogWindow(Deck!);

			if (result is not null)
			{
				foreach (var interval in result)
				{
					await MakeRepositoryRequestAsync(async () =>
					{
						var response = await repetitionIntervalRepository.UpdateRepetitionIntervalAsync(interval);

						if (response.IsFail)
						{
							MessageBoxService.Error(response.ErrorMessage);
							return;
						}
					});
				}

				await MakeRepositoryRequestAsync(async () =>
				{
					var deckResponse = await deckRepository.GetDeckByIdAsync(Deck!.Id);

					if (deckResponse.IsFail)
					{
						MessageBoxService.Error(deckResponse.ErrorMessage);
						return;
					}

					if (deckResponse.Data is { })
						Deck = deckResponse.Data;
				});
			}
		});

		#endregion

		/// <summary>
		/// Получить новые предложения
		/// </summary>
		/// <returns></returns>
		public async Task GetNewSentencesAsync()
		{
			if (Sentences.Count > 0)
				Sentences.Clear();

			await MakeRepositoryRequestAsync(async () =>
			{
				var response = await sentenceRepository.GetSentencesByDeckId(Deck!.Id);

				if (response.IsFail)
				{
					MessageBoxService.Error(response.ErrorMessage);
					return;
				}

				Sentences.AddRange(response.Data.OrderBy(n => n.Stage));
			});
		}

		/// <summary>
		/// Удалить файл
		/// </summary>
		/// <param name="path"></param>
		private void DeleteFile(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}
}
