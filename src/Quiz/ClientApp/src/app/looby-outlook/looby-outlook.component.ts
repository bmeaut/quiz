import { Component, OnInit } from '@angular/core';
import { UserState } from '../UserState';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-looby-outlook',
  templateUrl: './looby-outlook.component.html',
  styleUrls: ['./looby-outlook.component.css']  
})
export class LoobyOutlookComponent implements OnInit {

  q : UserState
  constructor(private route: ActivatedRoute) {
    this.q = this.route.snapshot.params.state
   }

  ngOnInit() {
    this.q = 2
   /* this.names.forEach(this.printElement)
   /* for(let i = 0; i < this.names.length; i++){
      let element = this.names[i]
      this.printElement(element)
    }*/
    let tag1 = document.createElement("div")
    tag1.textContent= this.names[0]

    let list1 = document.getElementById("playerlist")
    list1.appendChild(tag1)
    tag1.classList.add("player")

    let tag2 = document.createElement("div")
    tag2.classList.toggle("player", true)
    tag2.textContent= this.names[1]

    let list2 = document.getElementById("playerlist")
    list2.className = "player"
    list2.appendChild(tag2)
  }

  names: Array<string> = ['Andor', 'Csaba', 'Diána', 'Emil', 'Fiona'];

  printElement(element){
    let tag = document.createElement("div")
    let name = document.createTextNode(element)
   // tag.appendChild(name)   
    tag.textContent= element

    let list = document.getElementById("playerlist")
    let ch = document.getElementById("playerlist")
    tag.className = "player"
    list.appendChild(tag)
    list.className = "player"
   // list.innerHTML += tag.outerHTML
   // list.insertBefore(tag, list.childNodes[0]);
    
  }
}
