using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuizQuestion>> GetAll()
        {
            var questions = _quizService.GetAllQuestions();
            return Ok(questions);
        }

        [HttpPost("{id}/answer")]
        public ActionResult<object> CheckAnswer(int id, [FromBody] Answer answer)
        {
            if (!_quizService.TryCheckAnswer(id, answer.AnswerIndex, out var correct))
                return NotFound();

            return Ok(new { correct });
        }
    }
}
