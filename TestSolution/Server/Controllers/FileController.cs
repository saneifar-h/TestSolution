using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TestSolution.Shared;

namespace TestSolution.Server.Controllers
{
    [Route(CommonRoutes.FileUploadUrl)]
    public class FileController : ControllerBase
    {
        private const string SubFolder = "img/uploaded_imgs";
        private readonly string _filePath;
        private string _fileName;

        public FileController(IHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "wwwroot", SubFolder);
        }


        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }


        private string GetNewFileName()
        {
            _fileName = Path.GetRandomFileName().Split('.')[0] + ".jpg";
            var pathStr = Path.Combine(_filePath, _fileName);

            if (System.IO.File.Exists(pathStr)) return GetNewFileName();

            Console.WriteLine("Saving file to: {0}\n", pathStr);

            return pathStr;
        }

        [HttpPost]
        public async Task<IActionResult> SaveImg()
        {
            if (!Directory.Exists(_filePath)) Directory.CreateDirectory(_filePath);

            var pathStr = GetNewFileName();
            await using (var fs = System.IO.File.Create(pathStr))
            {
                await Request.Body.CopyToAsync(fs);
            }
            return Ok(Path.Combine(SubFolder, _fileName));
        }
    }
}