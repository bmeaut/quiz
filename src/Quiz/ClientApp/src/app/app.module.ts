import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { QuestionComponent } from './question/question.component';
import {ChartComponent} from './chart/chart.component';
import { StageComponent } from './stage/stage.component';
import { QuestionEditComponent } from './question-edit/question-edit.component';
import { QuestionListComponent } from './question-list/question-list.component';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { UsersComponent}  from './users/users.component';
import { LobbyComponent } from './lobby/lobby.component';
import { QuizUserComponent } from './quiz-user/quiz-user.component';
import { LoobyOutlookComponent } from './looby-outlook/looby-outlook.component';
import { UserBaseComponent } from './user-base/user-base.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    QuestionComponent,
    ChartComponent,
    StageComponent,
    QuestionEditComponent,
    QuestionListComponent,
    LeaderboardComponent,
    UsersComponent,
    StageComponent,
    QuestionEditComponent,
    QuestionListComponent,
    LobbyComponent,
    QuizUserComponent,
    LoobyOutlookComponent,
    UserBaseComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'question', component: QuestionComponent},
      { path: 'chart', component: ChartComponent},
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'stage', component: StageComponent },
      { path: 'question-edit', component: QuestionEditComponent },
      { path: 'question-list', component: QuestionListComponent },
      { path: 'leaderboard', component: LeaderboardComponent },
      { path: 'users', component: UsersComponent },
      { path: 'lobby', component: LobbyComponent },
      { path: 'lobby-outlook', component: LoobyOutlookComponent },
      { path: 'quiz-user', component: QuizUserComponent },
      { path: 'user-base', component: UserBaseComponent }

    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
