import { Injectable } from "@angular/core";
import { IBookLog } from "../interfaces/book-log.interface";
import { IBook } from "../interfaces/book.interface";

@Injectable({
    providedIn: 'root'
})
export class DateHelper {
    getCurrentTime(date: Date): string {
        var tzoffset = (date).getTimezoneOffset() * 60000;
        var localISOTime = (new Date(Date.now() - tzoffset)).toISOString().slice(0, -1);
        return localISOTime;
    }

}