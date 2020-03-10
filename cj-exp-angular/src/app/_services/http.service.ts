import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../_models/apiResponse.models';

@Injectable({ providedIn: 'root' })
export class HttpService {
  constructor(private http: HttpClient) {}

  getRequest(endpoint: string, params: any): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${environment.apiUrl}${endpoint}`);
  }
  postRequest(endpoint: string, params: any): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${environment.apiUrl}${endpoint}`, params);
  }
}
