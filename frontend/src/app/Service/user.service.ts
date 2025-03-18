// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Observable } from 'rxjs';

// @Injectable({
//   providedIn: 'root'
// })
// export class UserService {
//   private userApiUrl = 'http://localhost:5083/api/auth'; // Replace with your actual API URL
//   private postsApiUrl = 'http://localhost:5083/api/Posts'; // Replace with your actual API URL

//   constructor(private http: HttpClient) { }

//   getUserDetails(userId: string): Observable<any> {
//     return this.http.get<any[]>(`${this.userApiUrl}/${userId}`);
//   }

//   getUserPosts(userId: string): Observable<any[]> {
//     return this.http.get<any[]>(`${this.postsApiUrl}/ByUser/${userId}`);
//   }

//   updatePost(postId: string, formData: FormData): Observable<any> {
//     return this.http.put<any>(`${this.postsApiUrl}/${postId}`, formData);
//   }
//   deletePost(postId: string): Observable<any> {
//     return this.http.delete<any>(`${this.postsApiUrl}/${postId}`);
//   }
// }

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  deleteProfile(id: any) {
    throw new Error('Method not implemented.');
  }
  private userApiUrl = 'http://localhost:5083/api/auth'; // Replace with your actual API URL
  private postsApiUrl = 'http://localhost:5083/api/Posts'; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  getUserDetails(userId: string): Observable<any> {
    return this.http.get<any[]>(`${this.userApiUrl}/${userId}`);
  }
  // deleteUser(userId: string): Observable<any> {
  //   return this.http.delete<any>(`${this.userApiUrl}/deleteUser/${userId}`);
  // }

  getUserPosts(userId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.postsApiUrl}/ByUser/${userId}`);
  }

  updatePost(postId: string, formData: FormData): Observable<any> {
    return this.http.put<any>(`${this.postsApiUrl}/${postId}`, formData);
  }

  deletePost(postId: string): Observable<any> {
    return this.http.delete<any>(`${this.postsApiUrl}/${postId}`);
  }
}