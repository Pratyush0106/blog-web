// import { HttpInterceptorFn } from '@angular/common/http';

// export const authInterceptor: HttpInterceptorFn = (req, next) => {
//   const token = localStorage.getItem('token');
//   const cloneReq = req.clone(
//     {

//       setHeaders: {
//         Authorization: `Bearer ${token}`
//       }
//     }
//   ) 
//   return next(req);
// };
// import { HttpInterceptorFn } from '@angular/common/http';

// export const authInterceptor: HttpInterceptorFn = (req, next) => {
//   const token = localStorage.getItem('token');
  

//   if (token) {
//     const cloneReq = req.clone({
//       setHeaders: {
//         Authorization: `Bearer ${token}`
//       }
//     });
//     return next(cloneReq);
//   }
//   return next(req);
// };


import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token');

  if (token) {
    const cloneReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    return next(cloneReq).pipe(
      catchError((error) => {
        if (error.status === 401) {
          // Handle 401 error (e.g., redirect to login)
          console.error('Unauthorized request - redirecting to login');
          const router = new Router();
          router.navigate(['/login']);
        }
        throw error;
      })
    );
  }
  return next(req);
};