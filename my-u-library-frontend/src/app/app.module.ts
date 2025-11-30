import { NgModule } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        AppComponent
    ],
    exports: [],
    bootstrap: [AppComponent],
    imports: [
        ReactiveFormsModule,
        AppRoutingModule,
    ],
    providers: [provideHttpClient(withInterceptorsFromDi())]
})
export class AppModule { }