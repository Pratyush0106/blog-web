// import { Component } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { CreatePostService } from '../../Service/create-post.service';

// @Component({
//   selector: 'app-create-post',
//   templateUrl: './create-post.component.html',
//   styleUrls: ['./create-post.component.css'],
//   imports: [CommonModule, FormsModule, ReactiveFormsModule],
//   standalone: true
// })
// export class CreatePostComponent {
//   postData = {
//     title: '',
//     content: '',
//     genre: '',
//     imageUrl: ''
//   };

//   constructor(private createPostService: CreatePostService) {}

//   onSubmit() {
//     const formData = new FormData();
//     formData.append('title', this.postData.title);
//     formData.append('content', this.postData.content);
//     formData.append('genre', this.postData.genre);
//     formData.append('imageUrl', this.postData.imageUrl);

//     this.createPostService.createPost(formData).subscribe(
//       response => {
//         alert('Post created successfully');
//         console.log('Post created successfully', response);
//       },
//       error => {
//         alert('Error creating post');
//         console.error('Error creating post', error);
//       }
//     );
//   }
//   closeForm(){

//   }
// }

import { Component, AfterViewInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreatePostService } from '../../Service/create-post.service';
import Quill from 'quill';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  standalone: true
})
export class CreatePostComponent implements AfterViewInit {
  postData = {
    title: '',
    content: '',
    genre: '',
    imageUrl: ''
  };
  quillEditor!: Quill;
  router = inject(Router);

  constructor(private createPostService: CreatePostService) {}

  ngAfterViewInit() {
    this.quillEditor = new Quill('#editor', {
      theme: 'snow'
    });
  }

  onSubmit() {
    const content = this.quillEditor.root.innerHTML;
    const formData = new FormData();
    formData.append('title', this.postData.title);
    formData.append('content', content);
    formData.append('genre', this.postData.genre);
    formData.append('imageUrl', this.postData.imageUrl);

    this.createPostService.createPost(formData).subscribe(
      response => {
        alert('Post created successfully');
        console.log('Post created successfully', response);
      },
      error => {
        alert('Error creating post');
        console.error('Error creating post', error);
      }
    );
  }

  closeForm() {


    this.router.navigate(['/blog'])
  }
}