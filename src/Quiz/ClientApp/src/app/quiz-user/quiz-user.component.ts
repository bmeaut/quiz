import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { QueryParameterNames } from 'src/api-authorization/api-authorization.constants';
import { UserBaseComponent } from '../user-base/user-base.component';
import { UserState } from '../UserState';



@Component({
  selector: 'quiz-user',
  template: './quiz-user.component.html',
  styleUrls: ['./quiz-user.component.css']
})
export class QuizUserComponent implements  OnInit {

  userState: UserState
  
  constructor(private router: Router ) { }

  ngOnInit() {
    
    this.userState = 1
    switch (this.userState){
      case 1:
        this.router.navigate(['/user-base']);
        break;
      case 2:
        this.router.navigate(['/lobby-outlook']);
        break;
      case UserState.Question:
            
        break;
      case UserState.QuestionResult:
            
        break;
      case UserState.MainResult:
            
        break;
    }
  }

  UserBase(){
    this.userState = 1
  }

  UserLobby(){
    this.userState = 2
  }
}
