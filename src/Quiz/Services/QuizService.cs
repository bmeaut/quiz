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
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext _context;
        int currentQuestionId;
        private QuizHub quizHub;
        Question currentQuestion;

        public QuizService(ApplicationDbContext context) {
            _context = context;
            // ez nemjó -- questions = context.Questions.ToList();
        }

        public void setQuizClient(QuizHub qHub)
        {
            this.quizHub = qHub;
        }
        public int Start()
        {
            currentQuestionId = 1;
            QuizInstance quiz = new QuizInstance { State = QuizState.Start, QuestionId = _context.Questions.Find(currentQuestionId).Id };
            _context.Add(quiz);
            _context.SaveChanges();
            int quizInstanceId = quiz.Id;
            return quizInstanceId;
        }

        public void Next(int quizInstanceId)
        {
            QuizInstance quizInstance = _context.QuizInstances.Find(quizInstanceId);
            
            switch (quizInstance.State)
            {
                case QuizState.Start:
                    quizInstance.State = QuizState.Showquestion;
                    currentQuestion = this.getCurrentQuestion(quizInstance);
                    //quizHub.SendQuestion()
                    break;
                case QuizState.Showquestion:
                    quizInstance.State = QuizState.Showanswer;
                    currentQuestion = this.getCurrentQuestion(quizInstance);
                    foreach (var a in currentQuestion.Answers)
                    {
                            if (a.IsCorrect)
                            {
                                //quizHub.ShowAnswer(string question, UserAnswer[]{string answertext,bool isCorrect, int answerCount})
                                break;
                            }
                    }
                    break;
                case QuizState.Showanswer:
                    currentQuestion = this.getCurrentQuestion(quizInstance);
                    if (quizInstance.QuestionId == this.QuestionCount() || quizInstance.QuestionId > this.QuestionCount())
                    {
                        quizInstance.State = QuizState.Quizresult;
                        var ansIn = _context.AnswerInstances.Where(a => a.Id == quizInstance.AnswerInstance.Id);
                        var grouped = ansIn.GroupBy(a => a.User, a => a.Score, (key, value) => new { User = key, Score = value });
                        List<UserScore> scores = _context.AnswerInstances.GroupBy(a => a.User).Select(ai => new UserScore
                        { user = ai.Key, sumScore = ai.Sum(a => a.Score) }).OrderByDescending(a => a.sumScore).ToList();


                        //ShowQuestionResults(UserResult[10]{string name, int score})
                    }
                    else
                    {
                        quizInstance.QuestionId =+1;
                    }
                    break;
                case QuizState.Questionresult:
                    //Finish
                    break;
                case QuizState.Quizresult:
                    break;
                default: return;
            }
        }

        private int QuestionCount()
        {
            return _context.Questions.ToList().Count;
        }

        private Question getCurrentQuestion(QuizInstance quizInstance)
        {
            currentQuestion = (Question)_context.Questions.Include(q => q.Answers).Where(q => q.Id == quizInstance.QuestionId);
            return currentQuestion;
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
