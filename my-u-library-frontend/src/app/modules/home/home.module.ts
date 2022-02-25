import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { BooksComponent } from './books/books.component';
import { UsersComponent } from './users/users.component';
import { MaterialModule } from 'src/app/modules/material.module';
import { SearchComponent } from './search/search.component';
import { ReactiveFormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    BooksComponent,
    UsersComponent,
    SearchComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MaterialModule,
    ReactiveFormsModule
  ]
})
export class HomeModule { }
