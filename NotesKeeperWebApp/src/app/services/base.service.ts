import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

/*@Injectable({
  providedIn: 'root'
})*/
export abstract class BaseService {

  constructor() { }

  protected abstract get ApiRoute(): string;

  protected ConstructUrl(method: string): string {
    return environment.apiUrl + this.ApiRoute + method;
  }
}
