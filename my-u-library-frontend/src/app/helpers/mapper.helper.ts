import { Injectable } from "@angular/core";
import { IBookLog } from "../interfaces/book-log.interface";
import { IBook } from "../interfaces/book.interface";

@Injectable({
    providedIn: 'root'
})
export class MapperHelper {
    booksToBookLogList(books: IBook[], user: number): IBookLog[] {
        let booksLogs: IBookLog[] = [];
        books.forEach(i => {
            let bookLog = <IBookLog>{
                IdUser: user,
                IdBook: i.idBook,
                LoanedDate: new Date().toISOString()
            };
            booksLogs.push(bookLog);
        });
        return booksLogs;
    }

}