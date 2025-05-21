using Backend.Models;

namespace Backend.Services
{
    public class QuizService
    {
        private readonly List<QuizQuestion> _questions = new()
            {
                new QuizQuestion
                {
                    Id = 1,
                    Question = "What is a GitHub Action?",
                    Options = new List<string> { "CI/CD tool", "Database", "Text editor" },
                    CorrectOptionIndex = 0
                }
            };

        public IEnumerable<QuizQuestion> GetAllQuestions() => _questions;

        public bool TryCheckAnswer(int questionId, int answerIndex, out bool correct)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                correct = false;
                return false;
            }

            correct = question.CorrectOptionIndex == answerIndex;
            return true;
        }
    }  
}

