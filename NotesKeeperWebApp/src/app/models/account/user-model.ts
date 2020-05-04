import { Token } from './tokens/token';

export class UserModel {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  accessToken: Token;
  refreshToken: Token;
  role: any;
}
