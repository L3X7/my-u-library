import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './books/books.component';
import { HomeComponent } from './home.component';
import { SearchComponent } from './search/search.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
    {
      path: '',
      component: HomeComponent,
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
        }
      ]
    }
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class HomeRoutingModule { }
