import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

export interface Answer {
  id: number;
  name: string;
  questionID: number;
  isCorrect: boolean;
}

export interface Question {
  id: number;
  name: string;
  text: string;
  answers: Answer[];
}

@Component({
  selector: 'app-question-list',
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})

export class QuestionListComponent {

  public questions: Question[];
  public answers: Answer[];

  public lastActiveQuestion: Question = null;
  public selectedQuestion: Question = null;

  onSelect(question: Question): void {
    this.selectedQuestion = question;
    if (this.selectedQuestion === this.lastActiveQuestion) {
      this.selectedQuestion = null;
    }
    this.lastActiveQuestion = this.selectedQuestion;
  }

  delete(question: Question): void {
    this.questions = this.questions.filter(({ id }) => id !== question.id);
  }

  addQuestion(): void {
    this.router.navigate(['/question-edit']); 
  }

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    httpClient.get<Question[]>(baseUrl + 'api/Questions').subscribe(result => {
      this.questions = result;
    }, error => console.error(error));

    httpClient.get<Answer[]>(baseUrl + 'api/Answers').subscribe(result => {
      this.answers = result;
    }, error => console.error(error));
  }
}

