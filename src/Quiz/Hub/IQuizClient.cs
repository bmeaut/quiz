using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Hub
{
    public interface IQuizClient
    {
        Task ShowQuestion(int questionId);
        Task ShowAnswer(string answer, string user);
        Task ShowQuestionResult();
        Task UserJoined(string[] users);
        Task StartGame();
        Task Next();
    }
}
