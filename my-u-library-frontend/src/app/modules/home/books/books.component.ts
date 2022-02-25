import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { IBook } from 'src/app/interfaces/book.interface';
import { IGenre } from 'src/app/interfaces/genre.interface';
import { BookService } from 'src/app/services/book.service';
import { GenreService } from 'src/app/services/genre.service';
import { BookDialogComponent } from 'src/app/shared/ui-components/book-dialog/book-dialog.component';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  public genres: IGenre[] = [];
  displayedColumns: string[] = ['IdBook', 'Title', 'Author', 'PublishedYear', 'IdGenre', 'Quantity'];
  dataSource: MatTableDataSource<IBook> = new MatTableDataSource();
  constructor(private bookService: BookService, private dialog: MatDialog, private genreService: GenreService) { }

  ngOnInit(): void {
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

  loadGenres() : void{
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

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '400px';
    dialogConfig.data = this.genres;
    const dialogRef = this.dialog.open(BookDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => this.saveUser(data)
  ); 
  }


  saveUser( user: IBook){
    this.bookService.post(user).subscribe(
      (response) => {
        this.loadData();
      },
      (error) => {

      }
    )
  }

}
