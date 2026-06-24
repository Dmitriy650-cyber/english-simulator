namespace EnglishSimulator.Desktop.Services
{
	public static class FileService
	{
		/// <summary>
		/// Получить полный путь к аудиофалу
		/// </summary>
		/// <param name="audioFileName"></param>
		/// <returns></returns>
		public static string GetAudioFileFullPath(string audioFileName) =>
			Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AudioFiles", audioFileName);

		/// <summary>
		/// Удалить аудиофайл
		/// </summary>
		/// <param name="path"></param>
		public static void DeleteAudioFile(string path)
		{
			if (File.Exists(GetAudioFileFullPath(path)))
				File.Delete(GetAudioFileFullPath(path));
		}
	}
}
