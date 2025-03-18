import { Component, inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpErrorResponse, HttpClientModule } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-account',
  imports: [FormsModule, ReactiveFormsModule, HttpClientModule,],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css',
  host:{'skiphydration':'true'}
})
export class AccountComponent {
  isLoggedIn() {
    throw new Error('Method not implemented.');
  }
  registerForm: FormGroup;
  loginObj = { username: '', password: '' };
  http = inject(HttpClient);
  router = inject(Router);

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      fullname: ['', [Validators.required]],
      username: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  onRegister() {
    if (this.registerForm.valid) {
      const registerData = {
        fullname: this.registerForm.value.fullname,
        username: this.registerForm.value.username,
        email: this.registerForm.value.email,
        password: this.registerForm.value.password
      };

      this.http.post('http://localhost:5083/api/auth/signup', registerData).subscribe(
        (res: any) => {
          console.log(res); // Debugging log
          if (res && res.result) {
            alert('Registration Failed');
            this.router.navigate(['/account']);
          } else {
            alert('Registration Successfull');
          }
        },
        (error: HttpErrorResponse) => {
          console.error('Registration failed', error); // Debugging log
          alert('Registration Failed');
        }
      );
    }
  }
  

  onLogin() {
    this.http.post('http://localhost:5083/api/auth/login', this.loginObj).subscribe(
      (res: any) => {
        console.log(res); // Debugging log
        if (res && res.token) { // Check if the response contains a token
          alert('Login Successful');
          
          localStorage.setItem('token', res.token);
          localStorage.setItem('userId', res.id);
          localStorage.setItem('fullName', res.fullName);
          localStorage.setItem('email', res.email);
          localStorage.setItem('username', res.username);
          // localStorage.setItem('fullName', res.fullName.toString());
          // localStorage.setItem('email', res.email.toString());
          this.router.navigate(['/blog']);
        } else {
          alert('Login Failed');
        }
      },
      (error: HttpErrorResponse) => {
        console.error('Login failed', error); // Debugging log
        alert('Login Failed');
      }
      
    );
  }
  forgotPassword(): void {
    // Handle forgot password logic here
    console.log('Forgot Password button clicked');
    // You can navigate to a forgot password page or open a modal
    this.router.navigate(['/forgotpass']);
  }

}

// import { Component, inject } from '@angular/core';
// import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { HttpClient, HttpErrorResponse, HttpClientModule } from '@angular/common/http';
// import { Router, RouterLink } from '@angular/router';
// import { CommonModule } from '@angular/common';

// @Component({
//   selector: 'app-account',
//   imports: [FormsModule, ReactiveFormsModule, HttpClientModule, RouterLink, CommonModule],
//   templateUrl: './account.component.html',
//   styleUrl: './account.component.css',
//   host: { 'skiphydration': 'true' }
// })
// export class AccountComponent {
//   registerForm: FormGroup;
//   loginObj = { username: '', password: '' };
//   http = inject(HttpClient);
//   router = inject(Router);

//   constructor(private fb: FormBuilder) {
//     this.registerForm = this.fb.group({
//       fullname: ['', [Validators.required]],
//       username: ['', [Validators.required]],
//       email: ['', [Validators.required, Validators.email]],
//       password: ['', [Validators.required]]
//     });
//   }

//   onRegister() {
//     if (this.registerForm.valid) {
//       const registerData = {
//         fullname: this.registerForm.value.fullname,
//         username: this.registerForm.value.username,
//         email: this.registerForm.value.email,
//         password: this.registerForm.value.password
//       };

//       this.checkUsernameAndEmail(registerData.username, registerData.email).subscribe(
//         (exists: boolean) => {
//           if (exists) {
//             alert('Username or email is already taken');
//           } else {
//             this.http.post('http://localhost:5083/api/auth/signup', registerData).subscribe(
//               (res: any) => {
//                 console.log(res); // Debugging log
//                 if (res && res.result) {
//                   alert('Registration Successful');
//                 } else {
//                   alert('Registration Failed');
//                 }
//               },
//               (error: HttpErrorResponse) => {
//                 console.error('Registration failed', error); // Debugging log
//                 alert('Registration Failed');
//               }
//             );
//           }
//         },
//         (error: HttpErrorResponse) => {
//           console.error('Error checking username and email', error); // Debugging log
//           alert('Error checking username and email');
//         }
//       );
//     }
//   }

//   checkUsernameAndEmail(username: string, email: string) {
//     return this.http.post<boolean>('http://localhost:5083/api/auth/check-username-email', { username, email });
//   }

//   onLogin() {
//     this.http.post('http://localhost:5083/api/auth/login', this.loginObj).subscribe(
//       (res: any) => {
//         console.log(res); // Debugging log
//         if (res && res.token) { // Check if the response contains a token
//           alert('Login Successful');
//           localStorage.setItem('token', res.token);
//           this.router.navigate(['/blog']);
//         } else {
//           alert('Login Failed');
//         }
//       },
//       (error: HttpErrorResponse) => {
//         console.error('Login failed', error); // Debugging log
//         alert('Login Failed');
//       }
//     );
//   }
// }
