import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private apiUrl = 'http://localhost:5083/api/contact/submit'

  constructor(private http: HttpClient) { }

  contactUs(data: any) {
    return this.http.post(this.apiUrl, data);
  }
}
