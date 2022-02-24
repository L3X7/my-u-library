import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { USerService } from 'src/app/services/user.service';
import { IUser } from 'src/app/interfaces/user.interface';
import { UserDialogComponent } from 'src/app/shared/ui-components/user-dialog/user-dialog.component';
import { RoleService } from 'src/app/services/role.service';
import { IRole } from 'src/app/interfaces/role.interface';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  public isLoading: boolean = false;
  totalRows = 0;
  pageSize = 5;
  currentPage = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  public roles: IRole[] = [];
  displayedColumns: string[] = ['idUser', 'firstName', 'lastName', 'email', 'idRole'];
  dataSource: MatTableDataSource<IUser> = new MatTableDataSource();

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private userService: USerService, private dialog: MatDialog, private roleService: RoleService) { }

  ngAfterViewInit() {
    // this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.loadData();
    this.loadRoles();
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '400px';
    dialogConfig.data = this.roles;
    const dialogRef = this.dialog.open(UserDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => this.saveUser(data)
  );  

  }

  loadData() {
    this.userService.get().subscribe(
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


    // this.isLoading = true;
    // let URL = `http://localhost/database.php?pageno=${this.currentPage}&per_page=${this.pageSize}`;


    // fetch(URL)
    //   .then(response => response.json())
    //   .then(data => {
    //     this.dataSource.data = data.rows;
    //     setTimeout(() => {
    //       this.paginator.pageIndex = this.currentPage;
    //       this.paginator.length = data.count;
    //     });
    //     this.isLoading = false;
    //   }, error => {
    //     console.log(error);
    //     this.isLoading = false;
    //   });
  }


  loadRoles() : void{
    this.roleService.get().subscribe(
      (response) => {
        this.roles = response.data;
        setTimeout(() => {
          // this.paginator.pageIndex = this.currentPage;
          // this.paginator.length = response.count;
        });
      },
      (error) => {

      }
    );
  }

  saveUser( user: IUser){
    this.userService.post(user).subscribe(
      (response) => {
        this.loadData();
      },
      (error) => {

      }
    )
  }


  pageChanged(event: PageEvent) {
    console.log({ event });
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.loadData();
  }

}
