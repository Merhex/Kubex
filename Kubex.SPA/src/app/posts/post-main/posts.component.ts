import { Component, OnInit } from '@angular/core';
import { PostService } from '../../_services/post.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Post, Address } from '../../_models';
import { AlertService } from '../../_services';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostMainComponent implements OnInit {

  ngOnInit(): void {
  }

}
