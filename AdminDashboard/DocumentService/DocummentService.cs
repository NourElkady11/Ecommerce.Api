
namespace AdminDashboard.DocumentService
{
    public class DocummentService(ILogger<DocummentService> logger) : IDocummentService
    {
        public async Task<bool> DeleteFile(string PictureUrl, string folderName)
        {
            using var client = new HttpClient();
            var respone = await client.PostAsync($"https://localhost:5001/api/Documment/delete?PictureUrl={PictureUrl}&folderName={folderName}", null);
            var res = respone.EnsureSuccessStatusCode();
            var responceData = await respone.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public async Task<string> uploadFile(IFormFile file, string foldername)
        {
            try
            {
                using var client = new HttpClient();
                using var form = new MultipartFormDataContent();
                using var filecontent = new StreamContent(file.OpenReadStream());


                form.Add(filecontent, "file", file.FileName);
                //file is the name of the paramter on the controller 
                var respone = await client.PostAsync($"https://localhost:5001/api/Documment/Upload?folderName={foldername}", form);

                respone.EnsureSuccessStatusCode();
                return await respone.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
                throw;
            }

        }
    }

}
