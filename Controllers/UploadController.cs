using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FlashCardsAI.Controllers
{
    public class UploadController : Controller
    {
        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile uploadedFile)
        {
            // Sjekk om filen er lastet opp
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                ModelState.AddModelError("", "No file uploaded.");
                return View();
            }

            // Lagre filen midlertidig
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);  // Opprett mappen hvis den ikke eksisterer
            }

            var filePath = Path.Combine(uploadPath, uploadedFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyTo(stream);
            }

            // Send filen til PromptController for å behandle den og generere spørsmål
            return RedirectToAction("GenerateFromUpload", "Prompt", new { filePath });
        }
    }
}
