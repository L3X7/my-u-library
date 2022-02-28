import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

const SECRET_KEY = 'Ber1g0';
@Injectable({providedIn: 'root'})
export class CryptoService {
    constructor() { }
    
    public encrypt(data: any): any {
        const dataEncrypted = CryptoJS.AES.encrypt(data.toString(), SECRET_KEY);
        return dataEncrypted.toString();
    }

    public decrypt(data: any): any {
        const dataDecrypted = CryptoJS.AES.decrypt(data.toString(), SECRET_KEY).toString(CryptoJS.enc.Utf8);
        return dataDecrypted;
    }
}