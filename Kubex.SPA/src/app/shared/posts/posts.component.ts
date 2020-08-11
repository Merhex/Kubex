import { Component, OnInit } from '@angular/core';
import { ControlContainer } from '@angular/forms';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent {

  constructor(public controlContainer: ControlContainer) { }

}
