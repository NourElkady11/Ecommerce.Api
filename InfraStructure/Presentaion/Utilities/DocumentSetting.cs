using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public static class DocumentSetting
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

        public static bool DeleteFile(string pictureUtl, string foldername)
        {
            string Folderpath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\");
            
            var filepath=Path.Combine(pictureUtl, foldername);

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
                return true;
            }
            else { 
                return false;
            }


        }




    }
}
