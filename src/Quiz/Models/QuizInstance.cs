﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class QuizInstance
    {
        public int Id { get; set; }
        public QuizState State { get; set; }
        public int QuestionId { get; set; }
        public virtual AnswerInstance AnswerInstance { get; set; }
    }
        public enum QuizState
    {
        Start,
        Showquestion,
        Showanswer,
        Questionresult,
        Quizresult
    }
}
