import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

    constructor(private spinner: NgxSpinnerService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.spinner.show();
        return next.handle(req).pipe(
            tap(
                (event: HttpEvent<any>) => {
                    if (!(event instanceof HttpResponse)) {
                        return;
                    }

                    this.spinner.hide();
                },
                (error: Error) => {
                    this.spinner.hide();
                }
            )
        );
    }
}