import { TokenValidator } from './token-validator.service';
import { UserModel } from './../../models/account/user-model';
import { IValidator } from './ivalidator';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserValidator implements IValidator<UserModel> {

  constructor(private tokenValidator: TokenValidator) { }

  public IsValid(model: UserModel): boolean {
    return model
      && this.tokenValidator.IsValid(model.accessToken)
      && this.tokenValidator.IsValid(model.refreshToken);
  }
}
