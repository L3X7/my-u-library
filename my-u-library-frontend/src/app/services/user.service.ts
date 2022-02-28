import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../interfaces/user.interface';

@Injectable({providedIn: 'root'})
export class USerService {
    constructor(private http: HttpClient) { }

    get(){
        return this.http.get<any>(environment.urlApi.concat('user')).pipe(
            catchError(this.handleError)
        );
    }

    post(user: IUser){
        return this.http.post<any>(environment.urlApi.concat('user'), user).pipe(
            map(u => {
                localStorage.setItem("u", 'a');
            }),
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