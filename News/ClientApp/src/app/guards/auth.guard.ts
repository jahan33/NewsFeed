import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';


@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private auth: AuthService) { }
       isValid: boolean;
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    
        let menu = route.data["Access"] as string;
      
  //    //  var result = this.auth.HavePermission(menu)
  //return  this.auth.HavePermission(menu).then(result => {
  //          //
  //       this.isValid = true;
  //       return true;
    //      })
    return true;
      
    }
}
