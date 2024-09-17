using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FlashCardsAI.Models;
using System.Diagnostics;

namespace FlashCardsAI.Controllers
{
    public class PromptController : Controller
    {
        // Genererer flashcards fra opplastet fil
        public IActionResult GenerateFromUpload(string filePath)
        {
            // Sjekker om filen er tom eller ugyldig
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return BadRequest("File not found.");
            }

            // Kjører python-script ved hjelp av metoden under med opplastet fil og lagres i en streng
            string pythonOutput = RunPythonScript(filePath);

            // Parse JSON-output
            // Omdanner pythonOutput til et dynamisk c#
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(pythonOutput);

            // Deretter hent ut 'questions_and_answers' som en streng. 
            string questionsAndAnswersJson = jsonResponse.questions_and_answers.ToString();

            // Deserialiser 'questions_and_answers' som en liste av Prompt-objekter. Dette brukes til å vise flashcards. 
            List<Prompt> prompts = JsonConvert.DeserializeObject<List<Prompt>>(questionsAndAnswersJson) ?? new List<Prompt>();

            // Hvis listen er tom, send en tom liste. 
            if (prompts.Count == 0)
            {
                return View("Table", new List<Prompt>()); 
            }

            return View("Table", prompts);  // Returner listen "prompts" til view
        }

        // Privat metode som kjører Python-skriptet og returnerer output
        private string RunPythonScript(string filePath)
        {
            // Initialiserer en rom streng kalt "result". Brukes for å lagre output
            string result = string.Empty;

            try
            {
                string pythonPath = "/opt/anaconda3/envs/ppt_quiz/bin/python3";  // Sti til python3 som lar deg kjøre pptcScript.py (Må endres til der du har installert python)
                string scriptPath = "/Users/jonasbjerke/Documents/HomeHUB/Egne kode prosjekter/PowerpointFlashcards/pptxScript.py"; // Sti til python-script. Denne må endres hvis du kjører koden utenfor (Jonas) sin pc

                // Setter "kjøreregler" for python scriptet. 
                var psi = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\" \"{filePath}\"",  // Send filsti som argument til skriptet
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Starter/setter en variabel som starter "kjørereglene" (psi) 
                var process = Process.Start(psi);
                // Hvis pythonScriptet ikke er ferdig
                if (process != null)
                {
                    // Leser hele scriptet til slutt (ReadToEnd)
                    using (var reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }

                    // Fanger feilmeldinger og sender dette til python-konsollen dersom "error" er oppdaget
                    if (process.StandardError != null)
                    {
                        string error = process.StandardError.ReadToEnd();
                        if (!string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine("Python Error: " + error);
                        }
                    }
                    process.WaitForExit();
                }
            }
            // Fanger eventuelle feil i programmet
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return result;
        }
    }
}
