using Microsoft.AspNetCore.SignalR;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Hub
{
    public class QuizHub : Hub<IQuizClient>
    {

        public static HubRoom Lobby { get; } = new HubRoom();

        public class HubRoom
        {
            public string Name { get; set; }

            public List<User> users = new List<User>();

            public List<Question> questions = new List<Question>();
        }
    }
}
