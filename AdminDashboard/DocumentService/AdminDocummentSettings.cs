namespace AdminDashboard.DocumentService
{
	public class AdminDocummentSettings
	{
		public async static Task<string> uploadFile(IFormFile file, string foldername)
		{
			string Folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", foldername);

			string Filename = $"{Guid.NewGuid()}-{file.FileName}";

			string FilePath = Path.Combine(Folderpath, Filename);

			using var FileStream = new FileStream(FilePath, FileMode.Create);

			await file.CopyToAsync(FileStream);

			return $"images/{foldername}/{Filename}";


		}

		public static bool DeleteFile(string foldername, string filename)
		{
			string Filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", foldername, filename);

			if (File.Exists(Filepath))
			{
				File.Delete(Filepath);
				return true;
			}
			else
			{
				return false;
			}


		}

	}
}
