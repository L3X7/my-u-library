import { NgModule } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './modules/home/home.component';
import { FooterModule } from './shared/footer/footer.module';
import { NavbarModule } from './shared/navbar/navbar.module';
import { SidebarModule } from './shared/sidebar/sidebar.module';
import { Error404Component } from './shared/pages/error404/error404.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        Error404Component,
    ],
    exports: [],
    bootstrap: [AppComponent],
    imports: [
        ReactiveFormsModule,
        AppRoutingModule,
        SidebarModule,
        NavbarModule,
        FooterModule,],
    providers: [provideHttpClient(withInterceptorsFromDi())]
})
export class AppModule { }
