import { Component, OnInit } from '@angular/core';

export interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}

export const ROUTES: RouteInfo[] = [
  { path: '/home/users', title: 'Users', icon: 'bi-people', class: '' },
  { path: '/home/books', title: 'Books', icon: 'bi-book', class: '' },
  { path: '/home/search', title: 'Search', icon: 'bi-search', class: '' },
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public menuItems: any[] = [];
  constructor() { }

  ngOnInit(): void {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }

}
