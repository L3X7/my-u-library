// import { Component, Inject, OnInit } from '@angular/core';
// import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
// import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
// import { IGenre } from 'src/app/interfaces/genre.interface';
// @Component({
//   selector: 'app-book-dialog',
//   templateUrl: './book-dialog.component.html',
//   styleUrls: ['./book-dialog.component.scss']
// })
// export class BookDialogComponent implements OnInit {
//   public form!: UntypedFormGroup;
//   public genres: IGenre[] = [];
//   constructor(private fb: UntypedFormBuilder,
//     private dialogRef: MatDialogRef<BookDialogComponent>,
//     @Inject(MAT_DIALOG_DATA) data: any) {
//       this.genres = data;
//      }

//   ngOnInit(): void {
//     this.form = this.fb.group({
//       title: ['', Validators.required],
//       author: ['', Validators.required],
//       publishedYear: ['', [Validators.required, Validators.min(1)]],
//       idGenre: ['', Validators.required],
//       quantity: ['', [Validators.required, Validators.min(1)]],
//     });
//   }

//   save() {
//     if (this.form.valid) {
//       this.dialogRef.close(this.form.value);
//     }
//   }

//   close() {
//     this.dialogRef.close();
//   }

// }
