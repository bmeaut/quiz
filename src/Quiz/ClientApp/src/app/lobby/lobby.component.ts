import { Component, OnInit, Optional } from '@angular/core';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { HubBuilderService } from '../services/hub-builder.service';
import { Answer } from '../shared/answer';
import { Question } from '../shared/question';
import { abort } from 'process';
import { HttpClient } from '@angular/common/http';
import { QuestionCrudService } from '../shared/question-crud.service';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.css']
})
export class LobbyComponent implements OnInit {

  users: string[];

  questions: Question[];

  currentQuestion: Question;
  currentQuestionId: number;
  currentAnswers: Answer[];

  connection: signalR.HubConnection;

  constructor(private service: QuestionCrudService, hubBuilder: HubBuilderService, private router: Router) {

    this.connection = hubBuilder.getConnection();
    this.currentQuestionId = 0;
    

    this.connection.on("ShowQuestion", qId => this.showQuestion(qId));
    this.connection.on("ShowAnswer", (answer, user) => this.showAnswer(answer, user));
    this.connection.on("ShowResults", () => this.showQuizResults());
  }
  ngOnInit(): void {
    this.getQuestions();
    this.connection.start().then(() => {
      this.connection.invoke("SendQuestion", 0)
    });
  }

  nextQuestion() {

    var nextId = this.currentQuestionId + 1;

    if (nextId == this.questions.length) {
      this.connection.invoke("EndGame");
    }

    this.currentAnswers = null;
    this.connection.invoke("SendQuestion", this.currentQuestionId+1);
  }

  showQuestion(id: number) {
    const ids: string[] = ["answerA", "answerB", "answerC", "answerD"];

    this.currentQuestion = this.questions[id];

    this.currentQuestionId = id;

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

  showQuizResults() {
    this.router.navigate(['/stage']);
  }

  userJoined(username: string) {
    this.users.push(username);
  }

  getQuestions() {
    this.service.getQuestions().subscribe(resp => {
      this.questions = resp;
      console.log(this.questions[0].name);
    }, error => console.error(error));

    
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
      } else {
        answer = <HTMLInputElement>document.getElementById(id);
        answer.classList.remove('answer');
        answer.classList.add('answer-disabled');
        answer.disabled = true;
      }
    }


  }
}
