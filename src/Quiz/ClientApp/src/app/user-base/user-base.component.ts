import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { from } from 'rxjs';
import { UserState } from '../UserState';
import { QuizUserComponent } from '../quiz-user/quiz-user.component'

@Component({
  providers:[QuizUserComponent],
  selector: 'app-user-base',
  templateUrl: './user-base.component.html',
  styleUrls: ['./user-base.component.css']
})
export class UserBaseComponent implements OnInit {

  q : UserState
  constructor(private comp:QuizUserComponent) {
    
   }

  ngOnInit() {
    this.comp.UserBase()
  }

  
  JoinOnClick(){
    this.comp.UserLobby()
   // this.router.navigate(['/lobby-outlook']);
 }

}

