// import { provideRouter, Routes } from '@angular/router';
// import { HomeComponent } from './Component/home/home.component';
// import { AccountComponent } from './Component/account/account.component';
// import { BlogHomeComponent } from './Component/blog-home/blog-home.component';
// import { ProfileComponent } from './Component/profile/profile.component';
// import { CreatePostComponent } from './Component/create-post/create-post.component';
// import { UpdateComponent } from './Component/update/update.component';
// import { Component } from '@angular/core';
// import { UpdatePostComponent } from './Component/update-post/update-post.component';
// import { ForgotpassComponent } from './Component/forgotpass/forgotpass.component';
// import { ResetPasswordComponent } from './Component/reset-password/reset-password.component';
// import { ContactComponent } from './Component/contact/contact.component';
// import { AuthGuard } from './auth.guard';

// export const routes: Routes = [

//     { path: '',
//         redirectTo: 'home',
//         pathMatch: 'full'
//     },
//     {
//         path: 'home',
//         component: HomeComponent

//     },
//     {
//         path: 'account',
//         component: AccountComponent
//     },
//     {
//         path: 'blog',
//         component: BlogHomeComponent
//     },
//     {
//         path: 'profile',
//         component: ProfileComponent,
//         children: [
//         //   { path: 'create', component: CreatePostComponent },
//         //   { path: 'update', component: UpdateComponent },
//           { path: 'update-post/:id', component: UpdatePostComponent }
//         ]
// },
        
//     {
//         path: 'create',
//         component: CreatePostComponent
//     },
//     {
//         path: 'update',
//         component: UpdateComponent
//     },
//     {
//         path: 'forgotpass',
//         component: ForgotpassComponent,
       
//     },
//     {
//         path: 'reset',
//         component: ResetPasswordComponent
//     },
//     {
//         path:'contact',
//         component: ContactComponent
//     }
   
   
// ];


// export const appRoutingProviders: any[] = [];

// export const routing = provideRouter(routes);

import { provideRouter, Routes } from '@angular/router';
import { HomeComponent } from './Component/home/home.component';
import { AccountComponent } from './Component/account/account.component';
import { BlogHomeComponent } from './Component/blog-home/blog-home.component';
import { ProfileComponent } from './Component/profile/profile.component';
import { CreatePostComponent } from './Component/create-post/create-post.component';
import { UpdateComponent } from './Component/update/update.component';
import { UpdatePostComponent } from './Component/update-post/update-post.component';
import { ForgotpassComponent } from './Component/forgotpass/forgotpass.component';
import { ResetPasswordComponent } from './Component/reset-password/reset-password.component';
import { ContactComponent } from './Component/contact/contact.component';
import { AuthGuard } from './auth.guard'; // Adjust the path as necessary

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent},
  { path: 'account', component: AccountComponent},
  { path: 'blog', component: BlogHomeComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard], children: [
      { path: 'update-post/:id', component: UpdatePostComponent }
    ]
  },
  { path: 'create', component: CreatePostComponent, canActivate: [AuthGuard] },
  { path: 'update', component: UpdateComponent, canActivate: [AuthGuard] },
  { path: 'forgotpass', component: ForgotpassComponent },
  { path: 'reset', component: ResetPasswordComponent },
  { path: 'contact', component: ContactComponent, canActivate: [AuthGuard] }
];

export const appRoutingProviders: any[] = [];
export const routing = provideRouter(routes);