import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { IGenre } from 'src/app/interfaces/genre.interface';
@Component({
  selector: 'app-book-dialog',
  templateUrl: './book-dialog.component.html',
  styleUrls: ['./book-dialog.component.scss']
})
export class BookDialogComponent implements OnInit {
  public form!: FormGroup;
  public genres: IGenre[] = [];
  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<BookDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: any) {
      console.log(data);
      this.genres = data;
     }

  ngOnInit(): void {
    this.form = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      publishedYear: ['', Validators.required],
      idGenre: ['', Validators.required],
      quantity: ['', Validators.required],
    });
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  close() {
    this.dialogRef.close();
  }

}
