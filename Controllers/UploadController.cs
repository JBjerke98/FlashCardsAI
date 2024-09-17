using Microsoft.AspNetCore.Mvc;
namespace FlashCardsAI.Controllers
{
    //Controller for opplastning av pptx eller pdf
    public class UploadController : Controller
    {
        [HttpGet]
        //Returnerer UploadFile.cshtml (Get)
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        // Sender opplastet fil (Post)
        public IActionResult UploadFile(IFormFile uploadedFile)
        {
            // Sjekk om filen er lastet opp og om den inneholder noe
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                // Sender et "tomt" view
                ModelState.AddModelError("", "No file uploaded.");
                return View();
            }

            // Lagrer filen midlertidig i wwwroot/uploads. 
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            // Sjekker om mappen eksisterer
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);  // Opprett mappen hvis den ikke eksisterer
            }

            // Bruker filestream til å skrive filen til disk
            var filePath = Path.Combine(uploadPath, uploadedFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyTo(stream);
            }

            // Sender filen til PromptController slik at den kan bli behandlet
            return RedirectToAction("GenerateFromUpload", "Prompt", new { filePath });
        }
    }
}
