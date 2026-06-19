using System.Text.Json.Serialization;

namespace QuizzesApp.Models
{
    public class QuizData : IDisposable
    {
        [JsonPropertyName("quizzes")]
        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Quizzes.Clear();
                    Quizzes.TrimExcess();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~QuizData()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
            Console.WriteLine("Destructor called.");
        }

    }

    public class Quiz : IDisposable
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string IconPath { get; set; } = string.Empty;

        [JsonPropertyName("questions")]
        public List<Question> Questions { get; set; } = [];

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;

            // Cleanup code for 'Dispose()' method
            if (disposing)
            {
                Questions.Clear();
                Questions.TrimExcess();
            }

            disposedValue = true;
        }

        public void Dispose()
        {
            // Do not change this code. Put 
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~Quiz()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

    }

    public class Question
    {
        [JsonPropertyName("question")]
        public string QuestionStr { get; set; } = string.Empty;

        [JsonPropertyName("options")]
        public List<string> Options { get; set; } = new List<string>();

        [JsonPropertyName("answer")]
        public string AnswerStr { get; set; } = string.Empty;

    }

    public class QuestionSelection
    {
        public string? qName { get; set; }

        public string? qAnswer { get; set; }

        public string? id { get; set; }

    }

    public class QuizzesOverview
    {
        public List<QuizOverview> Quizzes { get; set; } = new List<QuizOverview>();

        public QuizzesOverview(List<QuizOverview>? quizzes)
        {
            Quizzes = quizzes;
        }
    }

    public class QuizOverview
    {
        public string Title { get; set; } = string.Empty;

        public string IconPath { get; set; } = string.Empty;

        public QuizOverview(Quiz quiz)
        {
            Title = quiz.Title;
            IconPath = quiz.IconPath;
        }
    }
}
