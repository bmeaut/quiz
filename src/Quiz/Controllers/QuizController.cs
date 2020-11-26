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

        [HttpGet("{id}")]
        public void Next(int id)
        {
            quizService.Next(id);
        }

        [HttpPost]
        public void SetAnswer(int quizInstanceId, int questionId, int answerId) 
        {
            //, HubConnectionContext connectionContext => , connectionContext.UserIdentifier
            quizService.SetAnswer(quizInstanceId, questionId, answerId,"dummyUser");
        }

    }
}
