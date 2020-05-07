import { UserValidator } from './../services/validators/user-validator.service';
import { AccountService } from 'src/app/services/account.service';
import { Observable } from 'rxjs';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class JwtTokenInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService, private userValidator: UserValidator) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    this.accountService.User
      .subscribe(user => {
        if (this.userValidator.IsValid(user)) {
          request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${user.accessToken.token}`
            }
        });
        }
      });

    return next.handle(request);
  }
}
