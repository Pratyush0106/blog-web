import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CreatePostService {
  private apiUrl = 'http://localhost:5083/api/Posts';

  constructor(private http: HttpClient) {}

  createPost(postData: FormData): Observable<any> {
    return this.http.post(this.apiUrl, postData);
  }
}