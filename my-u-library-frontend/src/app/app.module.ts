import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './modules/home/home.component';
import { FooterModule } from './shared/footer/footer.module';
import { NavbarModule } from './shared/navbar/navbar.module';
import { SidebarModule } from './shared/sidebar/sidebar.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserDialogComponent } from './shared/ui-components/user-dialog/user-dialog.component';
import { MaterialModule } from './modules/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { BookDialogComponent } from './shared/ui-components/book-dialog/book-dialog.component';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { Error404Component } from './shared/pages/error404/error404.component';
import { BookDetailDialogComponent } from './shared/ui-components/book-detail-dialog/book-detail-dialog.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';

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
    AppRoutingModule,
    SidebarModule,
    NavbarModule,
    FooterModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MaterialModule,
    NgxSpinnerModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
    }),
  ],
  exports: [

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true
    },
    {
			provide: LocationStrategy,
			useClass: HashLocationStrategy
		},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
