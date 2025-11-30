import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './modules/home/home.component';
import { FooterModule } from './shared/footer/footer.module';
import { NavbarModule } from './shared/navbar/navbar.module';
import { SidebarModule } from './shared/sidebar/sidebar.module';
import { UserDialogComponent } from './shared/ui-components/user-dialog/user-dialog.component';
import { MaterialModule } from './modules/material.module';
import { BookDialogComponent } from './shared/ui-components/book-dialog/book-dialog.component';
import { Error404Component } from './shared/pages/error404/error404.component';
import { BookDetailDialogComponent } from './shared/ui-components/book-detail-dialog/book-detail-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    UserDialogComponent,
    BookDialogComponent,
    Error404Component,
    BookDetailDialogComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    SidebarModule,
    NavbarModule,
    FooterModule,
    HttpClientModule,
    MaterialModule,
  ],
  exports: [],
  providers: [ ],
  bootstrap: [AppComponent]
})
export class AppModule { }
