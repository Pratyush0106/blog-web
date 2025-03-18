import { Component, inject, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css'],
  imports: [CommonModule, FormsModule,ReactiveFormsModule]
})
export class UpdateComponent implements OnInit {
  user = {
    fullName: '',
    email: '',
    password: ''
  };
  router = inject(Router);

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  onSubmit() {
    this.http.put('http://localhost:5083/api/auth/UpdateUser', this.user)
      .subscribe(response => {
        alert('User updated successfully!')
        console.log('User updated successfully!', response);
      }, error => {
        console.error('Error updating user', error);
      });
  }
  closeForm(){
    this.router.navigate(['/profile'])
  }

  
}