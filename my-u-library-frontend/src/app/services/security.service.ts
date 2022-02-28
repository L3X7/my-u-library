import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../interfaces/user.interface';
import { CryptoService } from './crypto.service';

@Injectable({providedIn: 'root'})
export class SecurityService {
    constructor(private http: HttpClient, private cryptoService: CryptoService) { }

    login(user: IUser){
        return this.http.post<any>(environment.urlApi.concat('security/login'), user).pipe(
            catchError(this.handleError)
        );
    }

    setData(data: any){
        const uR = this.cryptoService.encrypt(data.idRole);
        const uI = this.cryptoService.encrypt(data.idUser);
        const fN = this.cryptoService.encrypt(data.firstName);
        localStorage.setItem('uR', uR);
		localStorage.setItem('uI', uI);
        localStorage.setItem('fN', fN);
    }

    isAuthenticated() : boolean{
        const ui = localStorage.getItem('uI');
        if(ui){
            return true;
        }
        return false;
    }

    logOut(){
        localStorage.clear();
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