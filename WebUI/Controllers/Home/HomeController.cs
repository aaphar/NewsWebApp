using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebUI.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Home/UploadCKEditorImage")]
        public JsonResult UploadCKEditorImage(bool temp)
        {
            TempData["UploadedImagePath"] = null;

            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                var rError = new
                {
                    uploaded = false,
                    url = string.Empty
                };
                return Json(rError);
            }

            var formFile = files[0];
            var upFileName = formFile.FileName;

            var fileName = Path.GetFileNameWithoutExtension(upFileName) +
                DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(upFileName);

            var saveDir = @".\wwwroot\upload\";
            var savePath = saveDir + fileName;
            var previewPath = "/upload/" + fileName;

            bool result = true;
            try
            {
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }
                using (FileStream fs = System.IO.File.Create(savePath))
                {
                    formFile.CopyTo(fs);
                    fs.Flush();
                }

                if (temp)
                {
                    // Store the image URL in TempData
                    TempData["UploadedImagePath"] = previewPath;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            var rUpload = new
            {
                uploaded = result,
                url = result ? previewPath : string.Empty
            };
            return Json(rUpload);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
