import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let message = 'An unexpected error occurred.';

      switch (error.status) {
        case 0:
          message = 'Cannot reach the server. Check your connection.';
          break;
        case 400:
          message = 'Invalid request.';
          break;
        case 404:
          message = 'Resource not found.';
          break;
        case 500:
          message = 'Internal server error.';
          break;
      }

      console.error(`[HTTP Error] ${error.status} — ${message}`, error);
      return throwError(() => new Error(message));
    })
  );
};