import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { BooksComponent } from './books/books.component';
import { UsersComponent } from './users/users.component';
import { MaterialModule } from 'src/app/shared/modules/material.module';
@NgModule({
  declarations: [
    BooksComponent,
    UsersComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MaterialModule
  ]
})
export class HomeModule { }
