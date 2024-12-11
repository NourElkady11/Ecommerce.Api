using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class DocummentController:ApiController
    {


        [HttpPost("Upload")]
        public async Task<ActionResult<string>> UploadFile(IFormFile file,string folderName)
            => await DocumentSetting.uploadFile(file,folderName);



        [HttpPost("delete")]
        public ActionResult<bool> DeleteFile([FromQuery]string PictureUrl, [FromQuery] string folderName)
            =>DocumentSetting.DeleteFile(PictureUrl, folderName);



    }
}
