import { RefreshAccessTokenModel } from './../models/account/tokens/refresh-access-token-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegistrationModel } from '../models/account/registration-model';
import { Observable } from 'rxjs';
import { UserModel } from '../models/account/user-model';
import { LoginModel } from '../models/account/login-model';
import { map } from 'rxjs/operators';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
   }

  public Register(model: RegistrationModel): Observable<UserModel> {
    return this.httpClient
      .post<UserModel>(this.ConstructUrl('Register'), model);
  }

  public Login(model: LoginModel): Observable<UserModel> {
    return this.httpClient
      .post<UserModel>(this.ConstructUrl('Login'), model)
      .pipe(map(user => {
        if (user && user.accessToken) {
          this.StoreUser(user);
        }

        return user;
      }));
  }

  public Logout(): any {
    return this.httpClient
      .post<UserModel>(this.ConstructUrl('Login'), {})
      .pipe(map(user => {
        this.RemoveUser();
      }));
  }

  public RefreshAccessToken(): Observable<UserModel> {
    const refreshModel = new RefreshAccessTokenModel();
    this.User.subscribe(user => {
      refreshModel.accessToken = user.accessToken;
      refreshModel.refreshToken = user.refreshToken;
    });

    return this.httpClient.post<RefreshAccessTokenModel>(this.ConstructUrl('RefreshAccessToken'), refreshModel)
      .pipe<UserModel>(map(model => {
        if (model && model.accessToken && model.refreshToken) {
          const user = new UserModel();
          user.accessToken = model.accessToken;
          user.refreshToken = model.refreshToken;

          this.RemoveUser();
          this.StoreUser(user);

          return user;
        }

        return null;
      }));
  }

  public get User(): Observable<UserModel> {
    return new Observable(observer => {
      const user = localStorage.getItem('User');
      const model = user ? JSON.parse(user) : null;
      observer.next(model);
    });
  }

  protected get ApiRoute(): string {
    return '/api/Account/';
  }

  private RemoveUser(): void {
    localStorage.removeItem('User');
  }

  private StoreUser(user: UserModel): void {
    const stringUser = JSON.stringify(user);
    localStorage.setItem('User', stringUser);
  }
}
