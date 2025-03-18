

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { UserService } from '../../Service/user.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: any;
  posts: any[] = [];
  showModal: boolean = false;
  selectedPost: any;

  constructor(private userService: UserService, private http: HttpClient, private router: Router) { }


  ngOnInit(): void {
    const userId = localStorage.getItem('userId'); // Retrieve userId from local storage
    console.log('Retrieved User ID from local storage:', userId); // Debugging log
    if (userId) {
      this.userService.getUserDetails(userId).subscribe(data => {
        console.log('User data:', data); // Debugging log
        this.user = data;
        this.fetchUserPosts(userId);
      }, error => {
        console.error('Error fetching user details:', error);
      });
    } else {
      console.error('User ID is not available');
    }
  }

  fetchUserPosts(userId: string): void {
    console.log('Fetching posts for user ID:', userId);
    this.userService.getUserPosts(userId).subscribe(data => {
      console.log('User posts:', data); // Debugging log
      this.posts = data;
    }, error => {
      console.error('Error fetching user posts:', error);
    });
  }

  openUpdatePostModal(post: any): void {
    this.selectedPost = post; // Set the selected post
    this.showModal = true; // Show the modal
    console.log('Opening modal for post:', post); // Debugging log
  }

  closeUpdatePostModal(): void {
    this.showModal = false; // Hide the modal
    console.log('Closing modal'); // Debugging log
  }

  deletePost(post: any): void {
    console.log('Delete post:', post);
    this.userService.deletePost(post.id).subscribe(() => {
      this.posts = this.posts.filter(p => p.id !== post.id); // Remove the post from the local array
    });
  }

  deleteProfile(): void {
    const userId = this.user?.id || localStorage.getItem('userId'); // Use user ID from user object or local storage
    if (!userId) {
      console.error('User ID is not available');
      alert('User information is not available');
      return;
    }
  
    console.log('Delete profile for user ID:', userId);
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  
    this.http.delete(`http://localhost:5083/api/auth/deleteUser/${userId}`, { headers }).subscribe(
      response => {
        console.log('Profile deleted successfully:', response); // Debugging log
        alert('Profile deleted successfully');
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        localStorage.removeItem('username');
        this.router.navigate(['/account']);
      },
      (error: HttpErrorResponse) => {
        console.error('Error deleting profile:', error);
        alert('Failed to delete profile');
      }
    );

  
    
  }
  closeForm() {


    this.router.navigate(['/blog'])
  }
}