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
    this.connection = hubBuilder.getConnection("/lobby");
    this.connection.start();
    //Register server event handlers
    this.connection.on("UserJoined", user => this.userJoined(user));
    this.connection.on("ShowQuestion", qi => this.showQuestion());
    this.connection.on("ShowAnswer", (answer, user) => this.showAnswer(answer, user));
  }

  ngOnInit() {
  }

  nextQuestion() {

    this.currentAnswers = null;
    this.connection.invoke("ShowQuestion");
  }

  showQuestion() {
    const ids: string[] = ["answerA", "answerB", "answerC", "answerD"];
    let qi = new QuestionInstance("asd", ["aa", "bb", "cc", "dd"]);
    console.log(qi.question);
    console.log(qi.answers);
    this.currentQuestion = qi;
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
