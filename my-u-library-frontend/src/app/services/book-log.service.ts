import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, retry, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBookLog } from '../interfaces/book-log.interface';

@Injectable({ providedIn: 'root' })
export class BookLogService {
    constructor(private http: HttpClient) { }

    get() {
        return this.http.get<any>(environment.urlApi.concat('booklog')).pipe(
            retry(1),
            catchError(this.handleError)
        );
    }
    postList(booksLog: IBookLog[]) {
        return this.http.post<any>(environment.urlApi.concat('booklog/addBooksLog'), booksLog).pipe(
            retry(1),
            catchError(this.handleError)
        );
    }


    handleError(error: any) {
        let errorObj = {
            errorCode: null,
            errorMessage: ''
        };
        if (error.error instanceof ErrorEvent) {
            // Get client-side error
            errorObj.errorMessage = error.error.message;
        } else {
            // Get server-side error
            errorObj.errorCode = error.status;
            errorObj.errorMessage = error.message;

        }
        return throwError(errorObj);
    }
}