import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError,  throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBookLog } from '../interfaces/book-log.interface';

@Injectable({ providedIn: 'root' })
export class BookLogService {
    constructor(private http: HttpClient) { }

    get() {
        return this.http.get<any>(environment.urlApi.concat('booklog')).pipe(
            catchError(this.handleError)
        );
    }

    getBookReserved(idBook: number, idUser: number) {
        return this.http.get<any>(environment.urlApi.concat('booklog/getBookReserved/' + idBook + '/' + idUser)).pipe(
            catchError(this.handleError)
        );
    }


    filter(query: string) {
        return this.http.get<any>(environment.urlApi.concat('booklog/filter?', query)).pipe(
            catchError(this.handleError)
        );
    }

    postList(booksLog: IBookLog[]) {
        return this.http.post<any>(environment.urlApi.concat('booklog/addBooksLog'), booksLog).pipe(
            catchError(this.handleError)
        );
    }


    PatchBookLogAndBook(id: number, currentDate: string) {
        return this.http.patch<any>(environment.urlApi.concat('booklog/' + id),
            [{ "op": "replace", "path": "/returnedDate", "value": currentDate }]).pipe(
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