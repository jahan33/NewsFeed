import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { NewsService } from '../../services/news/news.service';
import { ToastrService } from 'ngx-toastr';
import { ChannelService } from '../../services/channel/channel.service';
import { LoginService } from 'src/app/services/auth/login.service';
import { SignupComponent } from '../signup/signup.component';

import { Overlay } from '@angular/cdk/overlay';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginData: any = {}
  errorMessage: any = {}



  //@ViewChild('f') loginForm: NgForm;

  public NewsData: any = {}
  constructor(public dialogRef: MatDialogRef<LoginComponent>,
    @Inject(MAT_DIALOG_DATA) data: any, private router: Router, public dialog: MatDialog, private overlay: Overlay,
    private route: ActivatedRoute,  public _AuthService: AuthService, public newsService: NewsService, public toastr: ToastrService, public channelService: ChannelService) {
    this.NewsData = data;
    this._AuthService.errorMessage = "";
    _AuthService.logout();
  
    this.loginData.Username = "";
    this.loginData.Password = "";
    this.errorMessage.error = "";
 
  }


  public showSignupPage(newsData: any = {}) {
    this.dialogRef.close();

    const scrollStrategy = this.overlay.scrollStrategies.noop();
    const dialogRef = this.dialog.open(SignupComponent, {
      width: '400px',
      data: this.NewsData,
      autoFocus: true,
      scrollStrategy
    });

    dialogRef.afterClosed().subscribe(result => {


    });
  }
  // On submit button click    
  onSubmit(form: NgForm) {
  
    if (form.invalid) {
      return;
    } else {
      this._AuthService.login(this.loginData).subscribe(

        result => {
          this.errorMessage = "";
          if (this.NewsData != null) {
            if (this.NewsData.Type == 'News') {
              this.newsService.SubscribeNews(this.NewsData.ID).subscribe(n => {

                this.channelService.GetChannel();
              
              })


            }
            if (this.NewsData.Type == 'Channel') {
              this.channelService.SubscribeChannel(this.NewsData.ID).subscribe(c => {

                this.newsService.SearchNews();
              })


            }


          } else {
            this.channelService.GetChannel();
            this.newsService.SearchNews();
          }
          this.dialogRef.close();
        },
        error => {
          
          this.errorMessage.error = error.error;
          
        });
    }

    // this.loginForm.reset();
  }

  ngOnInit() {
  }
}
