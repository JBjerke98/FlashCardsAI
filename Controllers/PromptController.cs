using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using FlashCardsAI.Models;
using System.Diagnostics;
using System.IO;

namespace FlashCardsAI.Controllers
{
    public class PromptController : Controller
    {
        // Denne metoden genererer flashcards fra opplastet fil
        public IActionResult GenerateFromUpload(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return BadRequest("File not found.");
            }

            // Kjør Python-skriptet med riktig filsti
            string pythonOutput = RunPythonScript(filePath);

            // Parse JSON-output
            // Først deserialiser hele JSON-objektet
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(pythonOutput);

            // Deretter hent ut 'questions_and_answers' som en streng
            string questionsAndAnswersJson = jsonResponse.questions_and_answers.ToString();

            // Deserialiser 'questions_and_answers' som en liste av Prompt-objekter
            List<Prompt> prompts = JsonConvert.DeserializeObject<List<Prompt>>(questionsAndAnswersJson) ?? new List<Prompt>();

            // Sjekk at listen ikke er null før du sender den til visningen
            if (prompts.Count == 0)
            {
                return View("Table", new List<Prompt>());  // Send en tom liste hvis det ikke finnes data
            }

            return View("Table", prompts);  // Returner data til view
        }

        // Privat metode som kjører Python-skriptet og returnerer output
        private string RunPythonScript(string filePath)
        {
            string result = string.Empty;

            try
            {
                string pythonPath = "/opt/anaconda3/envs/ppt_quiz/bin/python3";  // Oppdater med riktig sti
                string scriptPath = "/Users/jonasbjerke/Documents/HomeHUB/Egne kode prosjekter/PowerpointFlashcards/pptxScript.py"; // Path til ditt oppdaterte Python-skript

                var psi = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\" \"{filePath}\"",  // Send filsti som argument til skriptet
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = Process.Start(psi);
                if (process != null)
                {
                    using (var reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }

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
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return result;
        }
    }
}
