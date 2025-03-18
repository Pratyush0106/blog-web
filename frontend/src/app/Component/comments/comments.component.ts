import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class CommentsComponent implements OnInit {
  @Input()
  postId!: number;
  comments: any[] = [];
  newComment: string = '';
  userId: string = ''; // Assume this is set when the user logs in
  username: string = ''; // Assume this is set when the user logs in

  private commentsUrl = 'http://localhost:5083/api/comments';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId') || '';
    this.username = localStorage.getItem('username') || '';
    this.loadComments();
  }

  loadComments() {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    this.http.get<any[]>(`${this.commentsUrl}/post/${this.postId}`, { headers }).subscribe(
      (data: any[]) => {
        this.comments = data;
      },
      (error: HttpErrorResponse) => {
        if (error.status === 401) {
          console.error('Unauthorized access - invalid token:', error);
        } else {
          console.error('Error loading comments:', error);
        }
      }
    );
  }

  addComment() {
    const comment = {
      content: this.newComment,
      postId: this.postId,
      userId: parseInt(this.userId, 10), // Ensure userId is an integer
      username: this.username
    };

    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    this.http.post(this.commentsUrl, comment, { headers }).subscribe(
      response => {
        this.newComment = '';
        this.loadComments();
      },
      (error: HttpErrorResponse) => {
        console.error('Error adding comment:', error);
        if (error.status === 400) {
          console.error('Validation errors:', error.error.errors);
        }
      }
    );
  }

  deleteComment(commentId: number) {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    this.http.delete(`${this.commentsUrl}/${commentId}`, { headers }).subscribe(
      response => {
        this.loadComments();
      },
      (error: HttpErrorResponse) => {
        console.error('Error deleting comment:', error);
      }
    );
  }
}