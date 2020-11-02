using System.Collections.Generic;
using Quiz.Data;
using Quiz.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Quiz.Services
{
    public class QuizService
    {
        private readonly ApplicationDbContext _context;
        private List<Question> questions;
        private List<Answer> currentAnswers;

        public QuizService(ApplicationDbContext context) { _context = context; questions = context.Questions.ToList(); }
        public int Start()
        {
            QuizInstance quiz = new QuizInstance { State = QuizState.Start, QuestionId = questions[0].Id };
            _context.Add(quiz);
            _context.SaveChangesAsync();
            int QuizInstanceid = quiz.Id;
            return QuizInstanceid;
        }

        public void Next(int quizInstanceid)
        {
            QuizInstance qi = _context.QuizInstances.Find(quizInstanceid);
            switch (qi.State)
            {
                case QuizState.Start:
                case QuizState.Showquestion:
                    qi.State = QuizState.Showquestion;
                    currentAnswers = _context.Answers.Where(a => a.QuestionID == qi.QuestionId).ToList();
                    //ShowQuestion(int questionId, string question, Answer[4]{string answerText, int
                    break;
                case QuizState.Showanswer:
                    foreach (var a in currentAnswers)
                    {
                        if (a.IsCorrect)
                        {
                            //ShowAnswer(string question, UserAnswer[]{string answertext,bool isCorrect, int answerCount})
                            break;
                        }
                    }
                    break;
                case QuizState.Questionresult:

                    //ShowQuestionResults(UserResult[10]{string name, int score})
                case QuizState.Quizresult:
                default: return;
            }
        }

        public void SetAnswer(int quizInstanceId, int questionId, int answerId)
        {
           

        }

    }
}
