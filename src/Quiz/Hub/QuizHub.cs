﻿using Microsoft.AspNetCore.SignalR;
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

            public string Name { get; set; }



            public List<User> users = new List<User>();


        }

        public async Task EnterLobby()
        {
            var user = new User
            {
                UserId = r.Next(0,100).ToString(),
                Name = "user"+r.Next(0,100)
            };

            Lobby.users.Add(user);
            await Clients.Others.UserJoined(user);
            await Clients.All.SetUsers(Lobby.users);
            
        }

        public async Task Start()
        {
            
            await Clients.All.StartGame();
        }

        public async Task ShowAnswer(int questionId)
        {

        }

        public async Task SendQuestion(int questionId)
        {
            Console.WriteLine();
            Console.WriteLine("Calling ShowQuestion with id: "+questionId);
            await Clients.All.ShowQuestion(questionId);
        }
    }
}
