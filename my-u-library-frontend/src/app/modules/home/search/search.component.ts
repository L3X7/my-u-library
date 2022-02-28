import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MapperHelper } from 'src/app/helpers/mapper.helper';
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { IBook } from 'src/app/interfaces/book.interface';
import { IGenre } from 'src/app/interfaces/genre.interface';
import { BookService } from 'src/app/services/book.service';
import { GenreService } from 'src/app/services/genre.service';
import { BookLogService } from 'src/app/services/book-log.service';
import { ToastrService } from 'ngx-toastr';
import { BookDetailDialogComponent } from 'src/app/shared/ui-components/book-detail-dialog/book-detail-dialog.component';
import { CryptoService } from 'src/app/services/crypto.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  public displayedColumns: string[] = ['IdBook', 'Title', 'options'];
  public dataSource: MatTableDataSource<IBook> = new MatTableDataSource();

  public displayedColumnsTemp: string[] = ['Title', 'options'];
  public dataSourceTemp: MatTableDataSource<IBook> = new MatTableDataSource();
  public form!: FormGroup;
  public genres: IGenre[] = [];
  public booksStoraged: IBook[] = [];
  public uI: any;

  constructor(private bookService: BookService,
    private dialog: MatDialog,
    private fb: FormBuilder,
    private genreService: GenreService,
    private mapperHelper: MapperHelper,
    private bookLogService: BookLogService,
    private toastr: ToastrService,
    private cryptoService: CryptoService) { }

  ngOnInit(): void {
    this.uI = this.cryptoService.decrypt(localStorage.getItem('uI'));
    this.form = this.fb.group({
      title: [''],
      author: [''],
      genre: [''],
    });
    this.loadGenres();
  }


  openDialog(book: IBook) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '600px';
    dialogConfig.data = book;
    const dialogRef = this.dialog.open(BookDetailDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data) {
          this.add(data);
        }
      }
    );

  }


  // loadData() {
  //   this.bookService.get().subscribe(
  //     (response) => {
  //       this.dataSource.data = response.data;
  //       setTimeout(() => {
  //         // this.paginator.pageIndex = this.currentPage;
  //         // this.paginator.length = response.count;
  //       });
  //     },
  //     (error) => {
  //       this.toastr.error('An error ocurred', 'Notification');
  //     }
  //   );
  // }

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
        this.toastr.error('An error ocurred', 'Notification');
      }
    );
  }

  validateBookReserved(book: IBook) {
    this.bookLogService.getBookReserved(book.idBook, this.uI).subscribe(
      (response) => {
        this.dataSourceTemp.data = [...this.dataSourceTemp.data, book];
      },
      (error) => {
        this.toastr.error('This book is not available for you', 'Notification');
      }
    )
  }

  add(book: IBook) {
    if (this.dataSourceTemp.data.length > 0) {
      let bookRepetitive = this.dataSourceTemp.data.find(i => i.idBook == book.idBook);
      if (bookRepetitive) {
        this.toastr.warning('This book already exists', 'Notification');
        return;
      } else{
        this.validateBookReserved(book);
      }
    } else{
      this.validateBookReserved(book);
    }
  }

  remove(book: IBook) {
    this.dataSourceTemp.data = this.dataSourceTemp.data.filter((value, key) => {
      return value.idBook != book.idBook;
    });
  }

  search() {
    let data = this.form.getRawValue();
    this.bookService.filter(this.getQuery(data)).subscribe(
      (response) => {
        this.dataSource.data = response.data;
      },
      (error) => {
        this.toastr.error('An error ocurred', 'Notification');
      }
    );
  }


  getQuery(data: any): string {
    let myQuery: string = '';
    myQuery = (data.title != '' ? `title=${data.title}` : 'title=') + (data.author != '' ? `&author=${data.author}` : '&author=') + ((data.genre != '' && data.genre !== undefined) ? `&genre=${data.genre}` : '&genre=');
    return myQuery;

  }

  submit() {
    const bookLogs = this.mapperHelper.booksToBookLogList(this.dataSourceTemp.data, this.uI);
    this.bookLogService.postList(bookLogs).subscribe(
      (response) => {
        this.dataSourceTemp.data = [];
        this.search();
        this.toastr.success('Saved!', 'Notification');
      },
      (error) => {
        this.toastr.error('An error ocurred', 'Notification');
      }
    );
  }
}
