import { Component, Inject, OnInit } from '@angular/core';
import { Answer, Question, QuestionListComponent } from '../question-list/question-list.component';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-question-edit',
  templateUrl: './question-edit.component.html',
  styleUrls: ['./question-edit.component.css']
})

export class QuestionEditComponent implements OnInit {
  questionForm;
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {}
  

  addQuestion(): void {
    /*
     var a1: Answer = {
      id : 0,
      name : "válasz1!",
      questionID : 0,
      isCorrect : true
    }
    var a2: Answer = {
      id: 0,
      name: "válasz2!",
      questionID: 0,
      isCorrect: false
    }

    var q: Question = {
      id: 0,
      name: "matek",
      text: "kérdés?",
      answers : [a1, a2]
    };
    */
    this.router.navigate(['/question-list']);
  }
  ngOnInit() { }

}
