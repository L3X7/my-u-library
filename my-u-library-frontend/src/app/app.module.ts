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
import { BookDialogComponent } from './shared/ui-components/book-dialog/book-dialog.component';
import { LoadingInterceptor } from './interceptors/loading.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    UserDialogComponent,
    BookDialogComponent,
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
    NgxSpinnerModule
  ],
  exports: [

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
