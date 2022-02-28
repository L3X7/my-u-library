import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IBook } from 'src/app/interfaces/book.interface';

@Component({
  selector: 'app-book-detail-dialog',
  templateUrl: './book-detail-dialog.component.html',
  styleUrls: ['./book-detail-dialog.component.scss']
})
export class BookDetailDialogComponent implements OnInit {
  public book!: IBook;
  constructor( private dialogRef: MatDialogRef<BookDetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: any) {
      this.book = data;
     }

  ngOnInit(): void {
  }


  add() {
      this.dialogRef.close(this.book);
  }

  close() {
    this.dialogRef.close();
  }

}
