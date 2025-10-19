import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ApiResponseInterceptor implements HttpInterceptor {
  constructor(private toast: ToastrService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      tap(event => {
        if (event instanceof HttpResponse) {
          const body = event.body;
          if (body && typeof body === 'object' && body.hasOwnProperty('success')) {
            if (body.success && body.message) this.toast.success(body.message);
            if (!body.success && body.message) this.toast.error(body.message);
          }
        }
      }),
      catchError((err: HttpErrorResponse) => {
        const msg = (err.error && err.error.message) ? err.error.message : 'Erro inesperado na comunicação com a API.';
        this.toast.error(msg);
        return throwError(() => err);
      })
    );
  }
}
