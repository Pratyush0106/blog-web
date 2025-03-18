import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Post {
  userId: any;
  id: number;
  title: string;
  genre: string;
  content: string;
  imageUrl: string; // Use imageUrl instead of ImageUrl
}

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiUrl = 'http://localhost:5083/api/Posts';

  constructor(private http: HttpClient) {}

  getPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(this.apiUrl);
  }

  createPost(postData: FormData): Observable<Post> {
    return this.http.post<Post>(this.apiUrl, postData);
  }
}