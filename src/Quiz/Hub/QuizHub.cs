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

        Random r = new Random();

        public static HubRoom Lobby { get; } = new HubRoom 
        { 
            Name = "QuizRoom"
        
        };

        public class HubRoom
        {

            public HubRoom()
            {
                initQuestions();
            }

            public string Name { get; set; }


            public List<Question> questions = new List<Question>();



            public List<User> users = new List<User>();


            public void initQuestions()
            {
                
            }
        }

        public async Task EnterLobby()
        {
            var user = new User
            {
                UserId = r.Next(0,100).ToString(),
                Name = "user"+r.Next(0,100)
            };

            Lobby.users.Add(user);
            await Clients.Group(Lobby.Name).UserJoined(user);
            await Groups.AddToGroupAsync(user.UserId, Lobby.Name);
            await Clients.All.SetUsers(Lobby.users);
            
        }

        public async Task Start()
        {
            
            await Clients.All.StartGame();
        }

        public async Task ShowAnswer(int questionId)
        {

        }

        public async Task ShowQuestion(int questionId)
        {
            Question q = Lobby.questions[questionId];

            await Clients.All.ShowQuestion(q);
        }
    }
}
