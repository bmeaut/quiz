import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { HubBuilderService } from '../services/hub-builder.service';
import { Answer, UserAnswer, UserResult } from '../models/answer';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit {

  users: string[];

  connection: signalR.HubConnection;

  constructor(hubBuilder: HubBuilderService) {
    //Register server event handlers
    this.connection.on("UserJoined", users => this.userJoined(users));
  }

  ngOnInit() {
  }

  showQuestion(questionId: number, question: string, answer: Answer[]) {

  }

  showAnswer(question: string, userAnswer: UserAnswer[]) {

  }

  showQuestionResult(userResults: UserResult[]) {

  }

  showQuizResults(userResults: UserResult[]) {

  }

  userJoined(users: string[]) {

  }
}
