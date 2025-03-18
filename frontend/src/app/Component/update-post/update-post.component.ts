// import { Component, Input, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { HttpClient, HttpErrorResponse } from '@angular/common/http';
// import { CommonModule } from '@angular/common';
// import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { UserService } from '../../Service/user.service';
// import { from } from 'rxjs';

// @Component({
//   selector: 'app-update-post',
//   templateUrl: './update-post.component.html',
//   styleUrls: ['./update-post.component.css'],
//   imports: [CommonModule, FormsModule,ReactiveFormsModule]
// })
// export class UpdatePostComponent implements OnInit {
//   @Input() showModal: boolean = false;
//   updatePostForm: FormGroup;
//   selectedFile: File | null = null;

//   constructor(private fb: FormBuilder, private userService: UserService) {
//     this.updatePostForm = this.fb.group({
//       title: [''],
//       content: [''],
//       genre: [''],
//       image: [null]
//     });
//   }

//   ngOnInit(): void {}

//   onFileChange(event: any): void {
//     if (event.target.files.length > 0) {
//       this.selectedFile = event.target.files[0];
//     }
//   }

//   onSubmit(): void {
//     const formData = new FormData();
//     formData.append('title', this.updatePostForm.get('title')?.value);
//     formData.append('content', this.updatePostForm.get('content')?.value);
//     formData.append('genre', this.updatePostForm.get('genre')?.value);
//     if (this.selectedFile) {
//       formData.append('image', this.selectedFile);
//     }

//     const postId = localStorage.getItem('postId'); // Retrieve postId from local storage
//     if (postId) {
//       this.userService.updatePost(postId, formData).subscribe(response => {
//         console.log('Post updated successfully:', response);
//         this.closeModal();
//         // Refresh the posts list
//       }, error => {
//         console.error('Error updating post:', error);
//         console.error('Validation errors:', error.error.errors); // Log validation errors
//       });
//     }
//   }

//   closeModal(event?: any): void {
//     this.showModal = false;
//   }
// }

import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../Service/user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-post',
  templateUrl: './update-post.component.html',
  styleUrls: ['./update-post.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
})
export class UpdatePostComponent implements OnInit, OnChanges {
  @Input() showModal: boolean = false;
  @Input() post: any; // Assuming post is passed as an input
  updatePostForm: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.updatePostForm = this.fb.group({
      title: [{ value: '', disabled: true }, Validators.required],
      content: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    if (this.post) {
      this.updatePostForm.patchValue({
        title: this.post.title,
        content: this.post.content
      });
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['post'] && this.post) {
      this.updatePostForm.patchValue({
        title: this.post.title,
        content: this.post.content
      });
      console.log('Form updated with post data:', this.post); // Debugging log
    }
  }

  onSubmit(): void {
    if (this.updatePostForm.valid && this.post) {
      const updatedContent = this.updatePostForm.get('content')?.value;
      const formData = new FormData();
      formData.append('title', this.post.title); // Title is not changing, so use the existing title
      formData.append('content', updatedContent);

      const postId = this.post.id.toString(); // Assuming post has an id property
      this.userService.updatePost(postId, formData).subscribe(response => {
        console.log('Post updated successfully:', response);
        this.closeModal();
      }, error => {
        console.error('Error updating post:', error);
        console.error('Validation errors:', error.error.errors); // Log validation errors
      });
    }
  }

  closeModal(event?: any): void {
    if (event) {
      event.stopPropagation();
    }
    this.showModal = false;
    console.log('Modal closed'); // Debugging log
  }
}