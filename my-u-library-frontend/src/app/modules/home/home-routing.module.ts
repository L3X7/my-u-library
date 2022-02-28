import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { BookReturnComponent } from './book-return/book-return.component';
import { BooksComponent } from './books/books.component';
import { HomeComponent } from './home.component';
import { SearchComponent } from './search/search.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
    {
      path: '',
      component: HomeComponent,
      canActivate: [AuthGuard],
      children: [
        {
          path: 'books',
          component: BooksComponent
        },
        {
          path: 'users',
          component: UsersComponent
        },
        {
          path: 'search',
          component: SearchComponent
        },
        {
          path: 'book-return',
          component: BookReturnComponent
        }
      ]
    }
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class HomeRoutingModule { }
