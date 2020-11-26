﻿using System.Collections.Generic;
using Quiz.Data;
using Quiz.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Quiz.Hub;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.SignalR;

namespace Quiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext _context;
        private IHubContext<QuizHub> _hubContext;
        Question currentQuestion;
        int currentQuestionId;

        public QuizService(ApplicationDbContext context, IHubContext<QuizHub> qh)
        {
            _context = context;
            _hubContext = qh;
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
                    _context.SaveChanges();
                    //quizHub.SendQuestion()
                    break;
                case QuizState.Showquestion:
                    quizInstance.State = QuizState.Showanswer;
                    _context.SaveChanges();
                    currentQuestion = this.getCurrentQuestion(quizInstance);
                    break;
                case QuizState.Showanswer:
                    currentQuestion = this.getCurrentQuestion(quizInstance);
                    foreach (var a in currentQuestion.Answers)
                    {
                        if (a.IsCorrect)
                        {
                            //quizHub.ShowAnswer(string question, UserAnswer[]{string answertext,bool isCorrect, int answerCount})
                            break;
                        }
                    }
                    if (currentQuestion.Id == _context.Questions.Count())
                    {
                        quizInstance.State = QuizState.Questionresult;
                    }
                    else
                    {
                        quizInstance.State = QuizState.Showquestion;
                        quizInstance.QuestionId++;
                    }
                    _context.SaveChanges();
                    break;
                case QuizState.Questionresult:
                    quizInstance.State = QuizState.Quizresult;
                    _context.SaveChanges();
                    break;
                case QuizState.Quizresult:
                    currentQuestion = this.getCurrentQuestion(quizInstance);
                    if (quizInstance.QuestionId == this.QuestionCount() || quizInstance.QuestionId > this.QuestionCount())
                    {
                        /*
                         // egy masik approach
                         * quizInstance.State = QuizState.Quizresult;
                         var allAnswers = quizInstance.SubmittedAnswers.ToList();

                         var userScores = new Dictionary<string, int>();

                         foreach(var ans in allAnswers)
                         {
                             if (userScores.ContainsKey(ans.User))
                                 userScores[ans.User] = ans.Score;
                             else
                             {
                                 userScores.Add(ans.User, ans.Score);
                             }
                         }
                         var topPlayers = new Dictionary<string, int>();

                         //top 3 jatékos: i < 3
                         for (int i = 0; i < 3; i++)
                         {
                             var topscore = userScores.Values.Max();
                             var 
                             topPlayers.Add()
                         }*/

                        var ansIn = _context.AnswerInstances.ToList();
                        var grouped = ansIn.GroupBy(a => a.User, a => a.Score, (key, value) => new { User = key, Score = value });
                        List<UserScore> scores = _context.AnswerInstances.GroupBy(a => a.User).Select(ai => new UserScore
                        { user = ai.Key, sumScore = ai.Sum(a => a.Score) }).OrderByDescending(a => a.sumScore).ToList();

                        quizInstance.State = QuizState.Questionresult;
                        _context.SaveChanges();

                        //ShowQuestionResults(UserResult[10]{string name, int score})
                    }
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
            currentQuestion = _context.Questions.Where(q => q.Id == quizInstance.QuestionId).Include(q => q.Answers).SingleOrDefault();
            return currentQuestion;
        }

        public class UserScore
        {
            public string user { get; set; }
            public int sumScore { get; set; }
        }

        public void SetAnswer(int quizInstanceId, int questionId, int answerId, string userName)
        {
            bool isGoodANswer = false;
            var question = _context.Questions.Where(q=> q.Id == questionId).Include(question => question.Answers).FirstOrDefault();
            int correctAnswerId = -1;
            foreach (Answer a in question.Answers)
            {
                if (a.IsCorrect)
                {
                    correctAnswerId = a.Id;
                    break;
                }
            }
            if (answerId == correctAnswerId)
                isGoodANswer = true;

            var user = new User();
            user.Name = userName;
            var ai = new AnswerInstance
            { QuestionId = questionId, AnswerId = answerId, Score = isGoodANswer ? 1: 0, isCorrect = isGoodANswer, User = userName};
            _context.AnswerInstances.Add(ai);
            QuizInstance quizInstance = _context.QuizInstances.Find(quizInstanceId);
            //quizInstance.SubmittedAnswers.Add(ai);
            _context.SaveChanges();
        }

    }
}
