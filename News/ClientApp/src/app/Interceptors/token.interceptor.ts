import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { HttpResponse } from '@angular/common/http';
import { AuthService } from '../services/auth/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthService) { }
 
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let TokenID: string = this.auth.getToken();
        let contentType: boolean = request.headers.has("Content-Type")
        let HeaderOption: any = {}       
          
       
        if (contentType == false && TokenID != null && TokenID.length > 0) {
             request = request.clone({
                setHeaders: {
                       'Authorization': `Bearer ${AuthService.getToken()}`,
                },

            });
           
        }else if (TokenID != null && TokenID.length > 0) {
            request = request.clone({
                setHeaders: {
                    'Authorization': `Bearer ${AuthService.getToken()}`,
                },

            });
        }
       
       

      return next.handle(request).map(resp => {
        if (resp instanceof HttpResponse) {
        

        }
       
        return resp;
      });
    }
   
}
