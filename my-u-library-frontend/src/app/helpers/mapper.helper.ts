import { Injectable } from "@angular/core";
import { IBookLog } from "../interfaces/book-log.interface";
import { IBook } from "../interfaces/book.interface";
import { DateHelper } from "./date.helper";

@Injectable({
    providedIn: 'root'
})
export class MapperHelper {
    constructor(private dateHelper: DateHelper){}

    booksToBookLogList(books: IBook[], user: number): IBookLog[] {
        let booksLogs: IBookLog[] = [];
        books.forEach(i => {
            let bookLog = <IBookLog>{
                idUser: user,
                idBook: i.idBook,
                loanedDate: this.dateHelper.getCurrentTime(new Date())
            };
            booksLogs.push(bookLog);
        });
        return booksLogs;
    }
}