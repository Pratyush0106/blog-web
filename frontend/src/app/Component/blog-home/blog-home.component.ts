
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient, HttpHeaders } from '@angular/common/http';
import { PostService } from '../../Service/post.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommentsComponent } from "../comments/comments.component";

interface Post {
  id: number;
  imageUrl: string;
  title: string;
  genre: string;
  content: string;
  shortContent?: string;
  isExpanded?: boolean;
  comments?: Comment[];
}

interface Comment {
  id: number;
  postId: number;
  userId: number;
  content: string;
  username: string;
}

@Component({
  selector: 'app-blog-home',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterLink, ReactiveFormsModule, FormsModule, CommentsComponent],
  templateUrl: './blog-home.component.html',
  styleUrls: ['./blog-home.component.css']
})
export class BlogHomeComponent implements OnInit {
  blogPosts: Post[] = [];
  filteredPosts: Post[] = [];
  recentPosts: Post[] = [];
  expandedPost: Post | null = null;
  currentUserId: number = 1; // Replace with actual logged-in user ID
  searchQuery: string = '';
  username: string = '';

  constructor(private postService: PostService, private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.username = localStorage.getItem('username') || '';
    this.postService.getPosts().subscribe(posts => {
      this.blogPosts = posts.map(post => ({
        ...post,
        shortContent: post.content.split(' ').slice(0, 50).join(' ') + '...',
        isExpanded: false,
        comments: []
      }));
      this.filteredPosts = this.blogPosts;
      this.recentPosts = posts.slice(0, 3);
    });
  }

  search(): void {
    if (this.searchQuery === "") {
      this.filteredPosts = this.blogPosts;
    } else {
      this.filteredPosts = this.blogPosts.filter(post =>
        post.title.toLocaleLowerCase().includes(this.searchQuery.toLocaleLowerCase())
      );
    }
  }

  filterByCategory(category: string): void {
    this.filteredPosts = this.blogPosts.filter(post => post.genre.toLocaleLowerCase() === category.toLocaleLowerCase());
  }

  navigateToPost(postId: number): void {
    this.router.navigate(['', postId]);
  }

  expandPost(post: Post): void {
    this.expandedPost = post;
    // this.fetchComments(post.id);
  }

  collapsePost(): void {
    this.expandedPost = null;
  }

  logout(): void {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    this.router.navigate(['/account']);
  }
}