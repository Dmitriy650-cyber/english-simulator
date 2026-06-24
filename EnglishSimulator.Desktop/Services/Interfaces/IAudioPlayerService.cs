namespace EnglishSimulator.Desktop.Services.Interfaces
{
	public interface IAudioPlayerService
	{
		void PlayWavFile(string filePath);
		Task PlayWavFileAsync(string filePath, CancellationToken cancellationToken);
	}
}