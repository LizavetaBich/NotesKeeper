import { TokenValidator } from './../services/validators/token-validator.service';
import { UserValidator } from './../services/validators/user-validator.service';
import { AccountService } from 'src/app/services/account.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserModel } from '../models/account/user-model';
import { first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router,
              private accountService: AccountService,
              private userValidator: UserValidator,
              private tokenValidator: TokenValidator
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (this.User) {
      if (this.userValidator.IsValid(this.User)) {
        return true;
      } else if (this.tokenValidator.IsValid(this.User.refreshToken)
                  && !this.tokenValidator.IsValid(this.User.accessToken)) {
        this.accountService.RefreshAccessToken()
          .pipe(first())
          .subscribe(
            model => {},
            error => {
              this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
            });

        return true;
      }
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
    return false;
  }

  private get User(): UserModel {
    let user;
    this.accountService.User.subscribe(
      model => {
        user = model;
      }
    );
    return user;
  }
}
