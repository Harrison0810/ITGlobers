import { Injectable } from '@angular/core';
import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpErrorResponse,
    HttpHeaders
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Clone the request to add the new header if the request send to the api app
        let clonedRequest: HttpRequest<any>;

        // Get token
        const token = localStorage.getItem('ITGlobers-Token');

        // Create headers
        let headers: HttpHeaders = new HttpHeaders();

        if (token) {
            headers = headers.append('Authorization', 'Bearer ' + token);
        }
        clonedRequest = request.clone({ headers: headers });

        // Pass the cloned request instead of the original request to the next handle
        return next.handle(clonedRequest)
            .pipe(
                retry(0),
                catchError((error: HttpErrorResponse): Observable<HttpEvent<any>> => {
                    return throwError(error);
                })
            );
    }
}