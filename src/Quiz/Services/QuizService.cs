using System.Collections.Generic;
using Quiz.Data;
using Quiz.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Quiz.Hub;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Quiz.Services
{
    public class QuizService
    {
        private readonly ApplicationDbContext _context;
        private List<Question> questions;
        int currentQuestionId;
        private List<Answer> currentAnswers;
        private IQuizClient quizClient;

        public QuizService(ApplicationDbContext context, IQuizClient qc) { 
            _context = context;
            questions = context.Questions.ToList();
            quizClient = qc;
        }
        public int Start()
        {
            currentQuestionId = 1;
            QuizInstance quiz = new QuizInstance { State = QuizState.Start, QuestionId = questions[currentQuestionId].Id };
            _context.Add(quiz);
            _context.SaveChanges();
            int QuizInstanceid = quiz.Id;
            return QuizInstanceid;
        }

        public void Next(int quizInstanceid)
        {
            QuizInstance qi = _context.QuizInstances.Find(quizInstanceid);
            switch (qi.State)
            {
                case QuizState.Start:
                    qi.State = QuizState.Showquestion;
                    currentAnswers = _context.Answers.Where(a => a.QuestionID == qi.QuestionId).ToList();
                    //ShowQuestion(int questionId, string question, Answer[4]{string answerText, int
                    break;
                case QuizState.Showquestion:
                    qi.State = QuizState.Showanswer;
                    foreach (var a in currentAnswers)
                    {
                            if (a.IsCorrect)
                            {
                                //ShowAnswer(string question, UserAnswer[]{string answertext,bool isCorrect, int answerCount})
                                break;
                            }
                    }
                    break;
                case QuizState.Showanswer:
                    if (qi.QuestionId == questions.Count)
                    {

                        qi.State = QuizState.Quizresult;
                        var ansIn = _context.AnswerInstances.Where(a => a.Id == qi.AnswerInstance.Id);
                        var grouped = ansIn.GroupBy(a => a.User, a => a.Score, (key, value) => new { User = key, Score = value });
                        List<UserScore> scores = _context.AnswerInstances.GroupBy(a => a.User).Select(ai => new UserScore
                        { user = ai.Key, sumScore = ai.Sum(a => a.Score) }).OrderByDescending(a => a.sumScore).ToList();

                        //ShowQuestionResults(UserResult[10]{string name, int score})
                    }
                    else
                    {
                        currentQuestionId++;
                        qi.QuestionId = currentQuestionId;
                    }
                    break;
                case QuizState.Questionresult:
                    //Finish
                    
                case QuizState.Quizresult:
                default: return;
            }
        }

        public class UserScore
        {
           public User user { get; set; }
            public int sumScore { get; set; }
        }

        public void SetAnswer(int quizInstanceId, int questionId, int answerId, User user)
        {
            int score = 0;
            var question = _context.Questions.Find(questionId);
            int correctAnswerId = -1;
            foreach(Answer a in question.Answers)
            {
                if (a.IsCorrect)
                {
                    correctAnswerId = a.Id;
                    break;
                }
            }
            if (answerId == correctAnswerId)
                score = 1;

            var ai = new AnswerInstance { QuestionId = questionId, AnswerId = answerId, Score =score,isCorrect = false, User = user };
            _context.AnswerInstances.Add(ai);
            _context.SaveChanges();
        }

    }
}
