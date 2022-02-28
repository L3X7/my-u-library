import { Component, OnInit } from '@angular/core';
import { CryptoService } from 'src/app/services/crypto.service';
import { environment } from 'src/environments/environment';

export interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}

export const ROUTES_LIBRARY: RouteInfo[] = [
  { path: '/home/users', title: 'Users', icon: 'bi-people', class: '' },
  { path: '/home/books', title: 'Books', icon: 'bi-book', class: '' },
  { path: '/home/book-return', title: 'Return books', icon: 'bi-journal-arrow-down', class: '' },
];

export const ROUTES_STUDENT: RouteInfo[] = [
  { path: '/home/search', title: 'Search', icon: 'bi-search', class: '' },
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public menuItems: any[] = [];
  constructor(private cryptoService: CryptoService) { }

  ngOnInit(): void {
    const uR = this.cryptoService.decrypt(localStorage.getItem('uR'));
    if (uR == environment.roles.librarian) {
      this.menuItems = ROUTES_LIBRARY.filter(menuItem => menuItem);
    } else {
      this.menuItems = ROUTES_STUDENT.filter(menuItem => menuItem);
    }

  }

}
