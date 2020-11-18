using Microsoft.AspNetCore.SignalR;
using Quiz.Data;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Hub
{
    public class QuizHub : Hub<IQuizClient>
    {
        private readonly ApplicationDbContext _context;

        private List<string> users = new List<string>();
        Random r = new Random();

        public static HubRoom Lobby { get; } = new HubRoom();

        public class HubRoom
        {
            public string Name { get; set; }

            public List<User> users = new List<User>();

            public List<Question> questions = new List<Question>();
        }

        public async Task EnterLobby()
        {
            var user = "guestuser" + r.Next(0, 100);

            users.Add(user);
          
          await Clients.All.UserJoined(users.ToArray());
        }

        public Task Start()
        {
            return Clients.All.StartGame();
        }
    }
}
