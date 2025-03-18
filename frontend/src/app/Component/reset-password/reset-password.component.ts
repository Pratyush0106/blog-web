import { CommonModule } from '@angular/common';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-reset-password',
  imports: [FormsModule,CommonModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css',
 
})
export class ResetPasswordComponent {
  username: string = '';
  token: string = '';
  password: string = '';

  constructor(private http: HttpClient) {}

  onResetPassword(): void {
    const payload = {
      username: this.username,
      token: this.token,
      password: this.password
    };

    this.http.post('http://localhost:5083/api/auth/ResetPassword', payload).subscribe(
      (res: any) => {
        console.log('Password reset successful:', res);
        alert('Your password has been reset successfully.');
      },
      (error: HttpErrorResponse) => {
        console.error('Password reset failed:', error);
        alert('Failed to reset password. Please try again.');
      }
    );
  }
}


