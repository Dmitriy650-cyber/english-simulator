namespace EnglishSimulator.Desktop.Services
{
	public class AudioPlayerService : IAudioPlayerService, ISingletonDependency
	{
		public void PlayWavFile(string filePath)
		{
			using (var audioFile = new AudioFileReader(filePath))
			{
				using (var outputDevice = new WaveOutEvent())
				{
					outputDevice.Init(audioFile);
					outputDevice.Play();

					while (outputDevice.PlaybackState == PlaybackState.Playing)
					{
						Thread.Sleep(100);
					}
				}
			}
		}

		public async Task PlayWavFileAsync(string filePath, CancellationToken cancellationToken)
		{
			var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
			var stopped = false;

			var audioFile = new AudioFileReader(filePath);
			var outputDevice = new WaveOutEvent();

			outputDevice.PlaybackStopped += (_, args) =>
			{
				stopped = true;

				if (args.Exception != null)
					tcs.TrySetException(args.Exception);
				else
					tcs.TrySetResult(true);
			};

			outputDevice.Init(audioFile);
			outputDevice.Play();

			using var reg = cancellationToken.Register(() =>
			{
				if (!stopped)
					outputDevice.Stop();
			});

			try
			{
				await tcs.Task.ConfigureAwait(false);
			}
			finally
			{
				outputDevice.Dispose();
				audioFile.Dispose();
			}
		}
	}
}
