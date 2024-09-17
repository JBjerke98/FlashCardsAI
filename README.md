# Flashcards AI

Flashcards AI is a web application designed to simplify the creation of flashcards from your PDF and PowerPoint files. Using artificial intelligence, this app automatically extracts important questions and answers from uploaded files and presents them in an interactive flashcard format, making learning and knowledge-sharing easier.

## Features

-Upload PDF or PowerPoint: Users can upload .pdf or .pptx files, and the app will process and extract relevant content.

AI-Generated Flashcards:

-The app uses OpenAI's GPT-3.5-Turbo model to generate well-structured flashcards (questions and answers) from the uploaded files.

Interactive Flashcards:

-Once the AI generates flashcards, users can interact with them, flipping to view the answer to each question.

-Supports PDF and PPTX formats: The app can extract text from both PDF and PowerPoint files.

-Dynamic Flashcard Generation: The number of flashcards generated dynamically depends on the file content and user instruction.

## How It Works

-Upload: The user uploads a file (.pdf or .pptx).

-Processing: The backend extracts the text from the file.

-AI Question Generation: The text is passed to OpenAI's GPT-3.5-Turbo model, which generates a series of questions and answers.

-Flashcards Display: The generated questions and answers are shown in an interactive flashcard format on the web page.

## Prerequisites

-To run this project locally, ensure you have the following:

-.NET 6.0 or higher

-Python 3.x (preferably in a virtual environment)

-Python libraries:

-openai

-python-dotenv

-fitz (PyMuPDF for PDF processing)

-pptx (for PowerPoint files)

## Installation

### Clone the Repository

-git clone https://github.com/JBjerke98/flashcards-ai.git

-cd flashcards-ai

### Setup Python Environment

#### Create a virtual environment for Python:

-python -m venv venv

-source venv/bin/activate # For Windows use `venv\Scripts\activate`

-Python script is located in the repository

#### Install the required Python packages:

-pip install -r requirements.txt

-Create a .env file at the root of your project and add your OpenAI API key:

#### Create .env file

-OPENAI_API_KEY=your_openai_api_key_here

-Setup .NET Environment

-Navigate to the project folder:

-cd FlashCardsAI

## Restore the .NET dependencies:

-dotnet restore

-Build the project:

-dotnet build

## Running the Project

-Start the Python backend to process the files and interact with OpenAI:

-Ensure your virtual environment is active:

-source venv/bin/activate # For Windows, `venv\Scripts\activate`

-Run the Python script when a file is uploaded.

-Run the .NET Web App:

-dotnet run

-Open your browser and go to http://localhost:\*\*\*\* to interact with the web app.

## Project Structure

├── FlashCardsAI/

│ ├── Controllers/

│ │ ├── PromptController.cs # Handles AI-generated flashcards

│ │ └── UploadController.cs # Handles file uploads

│ ├── Models/

│ │ └── Prompt.cs # Model representing flashcards (questions and answers)

│ ├── Views/

│ │ ├── Prompt/

│ │ │ ├── Table.cshtml # View for displaying flashcards

│ │ └── Upload/

│ │ └── UploadFile.cshtml # View for uploading files

│ ├── wwwroot/

│ │ └── css/

│ │ └── layout.css # Custom CSS for styling the pages

├── pptxScript.py # Python script for extracting text and generating questions

├── .env # Environment file for OpenAI API key

├── README.md # This readme file

└── requirements.txt # Python package dependencies

## API Integration

-This project uses the OpenAI GPT-3.5-Turbo model for generating flashcards. Ensure that you have an API key from OpenAI, which you can acquire by signing up on the OpenAI platform.

## Known Issues

-Limited question generation for some documents.

-Potential parsing errors for non-standard text formats.

## Future Enhancements

-Support for more file types (e.g., Word documents).

-Additional customization options for generated flashcards.

-Improved error handling for complex documents.

## License

-This project is licensed under the MIT License.
