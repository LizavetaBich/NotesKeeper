import { AccountService } from 'src/app/services/account.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      if (err.status === 401) {
          // auto logout if 401 response returned from api
          this.accountService.Logout();
          location.reload(true);
      }

      const error = err.error.message || err.statusText;
      return throwError(error);
    }));
  }
}
