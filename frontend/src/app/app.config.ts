// import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
// import { provideRouter } from '@angular/router';
// import { provideHttpClient, withInterceptors } from '@angular/common/http';
// import { authInterceptor } from './Service/auth.interceptor';
// import { routes } from './app.routes';

// export const appConfig: ApplicationConfig = {
//   providers: [provideHttpClient(withInterceptors([authInterceptor])),  provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)]
// };
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './Service/auth.interceptor';
import { routes } from './app.routes';
import { AuthGuard } from './auth.guard';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([authInterceptor])),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    AuthGuard
  ]
};