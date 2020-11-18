import { Component, OnInit } from '@angular/core';
import { HubBuilderService } from '../services/hub-builder.service';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr'

@Component({
  selector: 'app-looby-outlook',
  templateUrl: './looby-outlook.component.html',
  styleUrls: ['./looby-outlook.component.css']
})
export class LoobyOutlookComponent implements OnInit {

  users: string[]

  connection: signalR.HubConnection

  constructor(private router: Router, hubbuilder: HubBuilderService) {
    this.users = [];
    this.connection = hubbuilder.getConnection("/lobby-outlook")

    this.connection.on("Start", () => this.startGame());
    this.connection.on("UserJoined", users => this.userJoined(users))
    this.connection.start().then(() => {
      this.connection.invoke("EnterLobby");
    });
    

  }
  ngOnInit(): void {
    
  }


  setUsers(users: string[]) {
    this.users = users
  }

  userJoined(users: string[]) {
    console.log("User joined!")
    this.users = users;
  }

  onStart() {
    this.connection.invoke("Start");
  }

  startGame() {
    this.router.navigate(['/lobby']);
  }



}
