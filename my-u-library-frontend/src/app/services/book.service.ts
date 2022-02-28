import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBook } from '../interfaces/book.interface';

@Injectable({providedIn: 'root'})
export class BookService {
    constructor(private http: HttpClient) { }

    get(){
        return this.http.get<any>(environment.urlApi.concat('book')).pipe(
            catchError(this.handleError)
        );
    }

    filter(query: string){
        return this.http.get<any>(environment.urlApi.concat('book/filter?', query)).pipe(
            catchError(this.handleError)
        );
    }

    post(book: IBook){
        return this.http.post<any>(environment.urlApi.concat('book'), book).pipe(
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