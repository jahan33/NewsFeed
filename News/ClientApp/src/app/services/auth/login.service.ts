import { Injectable } from '@angular/core';
import { LoginComponent } from '../../shared/login/login.component';
import { MatDialog } from '@angular/material';
import { Overlay } from '@angular/cdk/overlay';
import { SignupComponent } from 'src/app/shared/signup/signup.component';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(public dialog: MatDialog, private overlay: Overlay) { }
  // view news detail
  public showLoginPage(newsData: any = {}) {


    const scrollStrategy = this.overlay.scrollStrategies.noop();
    const dialogRef = this.dialog.open(LoginComponent, {
      width: '400px',
      data: newsData,
      autoFocus: true,
      scrollStrategy
    });

    dialogRef.afterClosed().subscribe(result => {


    });
  }
  public showSignupPage(newsData: any = {}) {


    const scrollStrategy = this.overlay.scrollStrategies.noop();
    const dialogRef = this.dialog.open(SignupComponent, {
      width: '400px',
      data: newsData,
      autoFocus: true,
      scrollStrategy
    });

    dialogRef.afterClosed().subscribe(result => {


    });
  }
  
}
