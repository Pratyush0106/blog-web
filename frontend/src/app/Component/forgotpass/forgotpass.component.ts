import { Component, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgotpass',
  templateUrl: './forgotpass.component.html',
  styleUrls: ['./forgotpass.component.css'],
  imports: [CommonModule,ReactiveFormsModule,FormsModule]
})
export class ForgotpassComponent {
  username: string = '';
  router = inject(Router);

  constructor(private http: HttpClient) {}

  onForgotPassword(): void {
    const payload = { username: this.username };

    this.http.post('http://localhost:5083/api/auth/ForgotPassword', payload).subscribe(
      (res: any) => {
        console.log('Forgot password request successful:', res);
        alert('Password reset instructions have been sent to your email.');
        this.router.navigate(['/reset']);
        
      },
      (error: HttpErrorResponse) => {
        console.error('Forgot password request failed:', error);
        alert('Failed to send password reset instructions. Please try again.');
      }
    );
  }
}