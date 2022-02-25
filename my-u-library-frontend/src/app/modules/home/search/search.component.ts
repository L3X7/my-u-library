import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MapperHelper } from 'src/app/helpers/mapper.helper';
import { IBook } from 'src/app/interfaces/book.interface';
import { IGenre } from 'src/app/interfaces/genre.interface';
import { BookService } from 'src/app/services/book.service';
import { GenreService } from 'src/app/services/genre.service';
import { BookLogService } from 'src/app/services/book-log.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  public displayedColumns: string[] = ['IdBook', 'Title', 'Author', 'PublishedYear', 'IdGenre', 'Quantity', 'options'];
  public dataSource: MatTableDataSource<IBook> = new MatTableDataSource();

  public displayedColumnsTemp: string[] = ['Title', 'options'];
  public dataSourceTemp: MatTableDataSource<IBook> = new MatTableDataSource();
  public form!: FormGroup;
  public genres: IGenre[] = [];
  public booksStoraged: IBook[] = [];

  constructor(private bookService: BookService, private fb: FormBuilder, private genreService: GenreService, private mapperHelper: MapperHelper, private bookLogService: BookLogService) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      title: [''],
      author: [''],
      genre: [''],
    });
    this.loadData();
    this.loadGenres();
  }


  loadData() {
    this.bookService.get().subscribe(
      (response) => {
        this.dataSource.data = response.data;
        setTimeout(() => {
          // this.paginator.pageIndex = this.currentPage;
          // this.paginator.length = response.count;
        });
      },
      (error) => {

      }
    );
  }

  loadGenres(): void {
    this.genreService.get().subscribe(
      (response) => {
        this.genres = response.data;
        setTimeout(() => {
          // this.paginator.pageIndex = this.currentPage;
          // this.paginator.length = response.count;
        });
      },
      (error) => {

      }
    );
  }

  add(book: IBook) {
    this.dataSourceTemp.data = [...this.dataSourceTemp.data, book];
  }

  remove(book: IBook) {
    this.dataSourceTemp.data = this.dataSourceTemp.data.filter((value, key) => {
      return value.idBook != book.idBook;
    });
    console.log(this.dataSourceTemp.data);
  }

  search() {
    let data = this.form.getRawValue();
    this.bookService.filter(this.getQuery(data)).subscribe(
      (response) => {
        this.dataSource.data = response.data;
      },
      (error) => {

      }
    )

  }


  getQuery(data: any): string {
    let myQuery: string = '';
    myQuery = (data.title != '' ? `title=${data.title}` : 'title=') + (data.author != '' ? `&author=${data.author}` : '&author=') + ((data.genre != '' && data.genre !== undefined) ? `&genre=${data.genre}` : '&genre=');
    return myQuery;

  }

  submit() {
    const bookLogs = this.mapperHelper.booksToBookLogList(this.dataSourceTemp.data, 1);
    this.bookLogService.postList(bookLogs).subscribe(
      (response) => {
        this.dataSourceTemp.data = [];
        this.loadData();

      },
      (error) => {

      }
    )
  }



}
