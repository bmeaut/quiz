import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { HubBuilderService } from '../services/hub-builder.service';
import { Answer, QuestionInstance, UserAnswer, UserResult } from '../models/answer';
import { abort } from 'process';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit {

  users: string[];

  currentQuestion: QuestionInstance;
  currentQuestionId: number;
  currentAnswers: string[];

  connection: signalR.HubConnection;

  constructor(hubBuilder: HubBuilderService) {
    this.currentQuestionId = 0;
    this.connection = hubBuilder.getConnection();
    this.connection.start().then(() => {
      this.connection.invoke("ShowQuestion", 0)
    });


    this.connection.on("ShowQuestion", qi => this.showQuestion(qi));
    this.connection.on("ShowAnswer", (answer, user) => this.showAnswer(answer, user));
  }

  ngOnInit() {
  }

  nextQuestion() {

    this.currentAnswers = null;
    this.connection.invoke("ShowQuestion");
  }

  showQuestion(q: QuestionInstance) {
    const ids: string[] = ["answerA", "answerB", "answerC", "answerD"];
    this.currentQuestion = q;
    let answer;
    for (let id of ids) {
      answer = <HTMLInputElement>document.getElementById(id);
      answer.classList.remove('answer-selected');
      answer.classList.add('answer');
      answer.disabled = false;
    }
  }

  showAnswer(answer: string, user: string) {
    
  }

  showQuestionResult(userResults: UserResult[]) {
    
  }

  showQuizResults(userResults: UserResult[]) {

  }

  userJoined(username: string) {
    this.users.push(username);
  }

  

  answerSelected(event: Event): void {
    const ids: string[] = ["answerA", "answerB", "answerC", "answerD"];
    const elementId: string = (event.target as Element).id;
    let answer;
    for (let id of ids) {
      if (id == elementId) {
        answer = <HTMLInputElement>document.getElementById(id);
        answer.classList.remove('answer');
        answer.classList.add('answer-selected');
        answer.disabled = true;
        this.connection.invoke("ShowAnswer", answer.innerText);
      } else {
        answer = <HTMLInputElement>document.getElementById(id);
        answer.classList.remove('answer');
        answer.classList.add('answer-disabled');
        answer.disabled = true;
      }
    }


  }
}
