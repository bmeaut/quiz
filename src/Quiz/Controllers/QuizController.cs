using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Data;
using Quiz.Services;
using Quiz.Hub;
using Microsoft.EntityFrameworkCore;


namespace Quiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly QuizService quizService;
        private readonly QuizHub quizHub;


        public QuizController(ApplicationDbContext context, QuizService qs, QuizHub qh)
        {
            _context = context;
            quizService = qs;
            quizHub = qh;
        }

        public int StartQuiz()
        {
           return quizService.Start();
        }

        public void Next(int quizInstanceId)
        {
            quizService.Next(quizInstanceId);
        }


        [HttpPost]
        public void SetAnswer(int quizInstanceId, int questionId, int answerId) 
        {

        
        }



    

    }
}
