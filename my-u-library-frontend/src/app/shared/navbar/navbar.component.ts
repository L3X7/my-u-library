import { Component, ElementRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CryptoService } from 'src/app/services/crypto.service';
import { SecurityService } from 'src/app/services/security.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  private toggleButton : any;
  private sidebarVisible: boolean;
  public isCollapsed = true;
  public name: any;
  constructor(private element : ElementRef,private router: Router, private securityService: SecurityService,
    private cryptoService: CryptoService) {
    this.sidebarVisible = false;
   }

  ngOnInit(): void {
    var navbar : HTMLElement = this.element.nativeElement;
    this.toggleButton = navbar.getElementsByClassName('navbar-toggle')[0];
    this.router.events.subscribe((event) => {
      this.sidebarClose();
   });
   this.name = this.cryptoService.decrypt(localStorage.getItem('fN'));
  }

  sidebarToggle() {
    if (this.sidebarVisible === false) {
        this.sidebarOpen();
    } else {
        this.sidebarClose();
    }
  }
  sidebarOpen() {
      const toggleButton = this.toggleButton;
      const html = document.getElementsByTagName('html')[0];
      const mainPanel =  <HTMLElement>document.getElementsByClassName('main-panel')[0];
      setTimeout(function(){
          toggleButton.classList.add('toggled');
      }, 500);

      html.classList.add('nav-open');
      if (window.innerWidth < 991) {
        mainPanel.style.position = 'fixed';
      }
      this.sidebarVisible = true;
  };
  sidebarClose() {
      const html = document.getElementsByTagName('html')[0];
      const mainPanel =  <HTMLElement>document.getElementsByClassName('main-panel')[0];
      if (window.innerWidth < 991) {
        setTimeout(function(){
          mainPanel.style.position = '';
        }, 500);
      }
      this.toggleButton.classList.remove('toggled');
      this.sidebarVisible = false;
      html.classList.remove('nav-open');
  };
  collapse(){
    this.isCollapsed = !this.isCollapsed;
    const navbar = document.getElementsByTagName('nav')[0];
    let subMenu = document.getElementById('collapseSubMenu');
    if (!this.isCollapsed) {
      if (subMenu) subMenu.classList.add('show');
      navbar.classList.remove('navbar-transparent');
      navbar.classList.add('bg-white');
    }else{
      if (subMenu) subMenu.classList.remove('show');
      navbar.classList.add('navbar-transparent');
      navbar.classList.remove('bg-white');
    }
  }

  logOut(){
    this.securityService.logOut();
    this.router.navigate(['/security/login']);
  }
}
