import { IGenre } from "./genre.interface";

export interface IBook {
    idBook: number;
    title: string;
    author: string;
    publishedYear: string;
    idGenre: number;
    genre: IGenre
    quantity: number;
}