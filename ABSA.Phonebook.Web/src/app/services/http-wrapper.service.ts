import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ResultModel } from '../Models/result.model';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpWrapperService<T> {
  private _url: string = environment.url;
  options = {
    headers: { 'content-type': 'application/json', accept: '*/*' },
  };

  constructor(private http: HttpClient) {}

  get(path: string) {
    return this.http
      .get<ResultModel<T>>(this._url + path)
      .pipe(catchError(this.handleError));
  }
  getByType<Y>(path: string) {
    return this.http
      .get<ResultModel<Y>>(this._url + path)
      .pipe(catchError(this.handleError));
  }

  put(path: string, data: any) {
    return this.http
      .put<ResultModel<T>>(this._url + path, JSON.stringify(data), this.options)
      .pipe(catchError(this.handleError));
  }

  post(path: string, data: any) {
    return this.http
      .post<ResultModel<T>>(
        this._url + path,
        JSON.stringify(data),
        this.options
      )
      .pipe(catchError(this.handleError));
  }

  delete(path: string, id: string) {
    return this.http
      .delete<ResultModel<boolean>>(`${this._url}${path}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `,
        error.error
      );
    }
    // Return an observable with a user-facing error message.
    return throwError('Something bad happened; please try again later.');
  }
}
