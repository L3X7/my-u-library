import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { IBookLog } from 'src/app/interfaces/book-log.interface';
import { BookLogService } from 'src/app/services/book-log.service';
import { ToastrService } from 'ngx-toastr';
import { DateHelper } from 'src/app/helpers/date.helper';

@Component({
  selector: 'app-book-return',
  templateUrl: './book-return.component.html',
  styleUrls: ['./book-return.component.scss']
})
export class BookReturnComponent implements OnInit {
  public displayedColumns: string[] = ['Title', 'User', 'Email', 'LoanedDate', 'options'];
  public dataSource: MatTableDataSource<IBookLog> = new MatTableDataSource();
  public form!: FormGroup;
  constructor(private bookLogService: BookLogService, private fb: FormBuilder, private toastr: ToastrService, private dateHelper: DateHelper) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      firstName: [''],
      LastName: [''],
      email: [''],
    });
  }


  submit(idBook: number) {
    const currentDate = this.dateHelper.getCurrentTime(new Date());
    this.bookLogService.PatchBookLogAndBook(idBook, currentDate).subscribe(
      (response) => {
        this.dataSource.data = [];
        this.search();
        this.toastr.success('Book returned!', 'Notification');

      },
      (error) => {
        this.toastr.error('An error ocurred', 'Notification');
      }
    );
  }

  getQuery(data: any): string {
    let myQuery: string = '';
    myQuery = (data.firstName != '' ? `firstName=${data.firstName}` : 'firstName=') + (data.LastName != '' ? `&LastName=${data.LastName}` : '&LastName=') + ((data.email != '') ? `&email=${data.email}` : '&email=');
    return myQuery;

  }

  search() {
    let data = this.form.getRawValue();
    this.bookLogService.filter(this.getQuery(data)).subscribe(
      (response) => {
        this.dataSource.data = response.data;
      },
      (error) => {

      }
    )

  }

}
