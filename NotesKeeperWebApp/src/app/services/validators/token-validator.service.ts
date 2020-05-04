import { IValidator } from './ivalidator';
import { Injectable } from '@angular/core';
import { Token } from 'src/app/models/account/tokens/token';

@Injectable({
  providedIn: 'root'
})
export class TokenValidator implements IValidator<Token> {

  constructor() { }

  public IsValid(model: Token): boolean {
    if (!model && !model.token) {
      return false;
    }

    const now = new Date(Date.now());
    const expiration = new Date(model.expiration);

    return expiration > now;
  }
}
