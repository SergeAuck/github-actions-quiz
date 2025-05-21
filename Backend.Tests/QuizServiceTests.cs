using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Xunit;

namespace Backend.Tests
{
    public class QuizServiceTests
    {
        private readonly QuizService _quizService;

        public QuizServiceTests()
        {
            _quizService = new QuizService();
        }

        [Fact]
        public void GetAllQuestions_ReturnsQuestions()
        {
            // Act
            var questions = _quizService.GetAllQuestions().ToList();

            // Assert
            Assert.NotNull(questions);
            Assert.NotEmpty(questions);
            Assert.Equal(1, questions.Count);
            Assert.Equal("What is a GitHub Action?", questions[0].Question);
        }

        [Fact]
        public void TryCheckAnswer_CorrectAnswer_ReturnsTrueAndCorrectIsTrue()
        {
            // Arrange
            var question = _quizService.GetAllQuestions().First();
            int correctIndex = question.CorrectOptionIndex;

            // Act
            var result = _quizService.TryCheckAnswer(question.Id, correctIndex, out bool correct);

            // Assert
            Assert.True(result);
            Assert.True(correct);
        }

        [Fact]
        public void TryCheckAnswer_WrongAnswer_ReturnsTrueAndCorrectIsFalse()
        {
            // Arrange
            var question = _quizService.GetAllQuestions().First();
            int wrongIndex = (question.CorrectOptionIndex + 1) % question.Options.Count;

            // Act
            var result = _quizService.TryCheckAnswer(question.Id, wrongIndex, out bool correct);

            // Assert
            Assert.True(result);
            Assert.False(correct);
        }

        [Fact]
        public void TryCheckAnswer_InvalidQuestionId_ReturnsFalseAndCorrectIsFalse()
        {
            // Act
            var result = _quizService.TryCheckAnswer(-1, 0, out bool correct);

            // Assert
            Assert.False(result);
            Assert.False(correct);
        }
    }
}
