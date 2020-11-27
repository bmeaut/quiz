using Microsoft.AspNetCore.Mvc;
using Quiz.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Quiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService quizService;

        public QuizController(IQuizService qs)
        {
            quizService = qs;
        }

        [HttpGet]
        public async Task<int> StartQuiz()
        {
            return quizService.Start();
        }

        [HttpGet("next/{id}")]
        public void Next(int id)
        {
            quizService.Next(id);
        }

        [HttpPost]
        public void SetAnswer(int[] parameters) 
        {
            //, HubConnectionContext connectionContext => , connectionContext.UserIdentifier
            quizService.SetAnswer(parameters[0], parameters[1], parameters[2],"dummyUser");
        }

        public class AnswerSubmit{
            public int quizId { get; }
            public int questionId { get; }
            public int answerId { get; }
        }

    }
}
