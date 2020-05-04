import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class EventService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super();
  }

  protected get ApiRoute(): string {
    return 'api/Event';
  }
}
