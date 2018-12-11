import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute } from "@angular/router";
import 'rxjs/add/operator/catch';
import { AuthService } from '../services/auth/auth.service';


declare var $: any;
@Injectable()
export class ResponseInterceptor implements HttpInterceptor {
    constructor(private router: Router,
      private route: ActivatedRoute, public auth: AuthService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      
      return next.handle(req).map(resp => {
       
            return resp;
      }).catch((err: HttpErrorResponse) => {
     
        if (err.status == 401) {
          localStorage.removeItem("userInfo");
          localStorage.clear();  
          
         
        }
        this.auth.errorMessage = err.error;
          
          
            return Observable.throw(err);
        });
    }
}
