import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { NgForm, Validators, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NewsService } from '../../services/news/news.service';
import { ToastrService } from 'ngx-toastr';
import { ChannelService } from '../../services/channel/channel.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  loginData: any = {}
  errorMessage: any = {}



  //@ViewChild('f') loginForm: NgForm;

  public NewsData: any = {}
  constructor(public dialogRef: MatDialogRef<SignupComponent>,
    @Inject(MAT_DIALOG_DATA) data: any, private router: Router,
    private route: ActivatedRoute, public _AuthService: AuthService, public newsService: NewsService, public toastr: ToastrService, public channelService: ChannelService) {
    this.NewsData = data;
    this._AuthService.errorMessage = "";
    _AuthService.logout();
    this.loginData.FullName = "";
    this.loginData.ConfirmPassword = "";
    
    this.loginData.Email = "";
    this.loginData.Password = "";
    this.errorMessage.error = "";
  
  }
  public setValidation(form: NgForm) {


    form.controls['Email'].setValidators([
      Validators.required,
      Validators.email
    ])
    form.controls['ConfirmPassword'].setValidators([Validators.compose(
      [Validators.required, this.validateAreEqual.bind(this, form),]
    )])
    form.controls['Password'].setValidators([Validators.compose(
      [Validators.required, this.validateLength.bind(this, form)]
    )])
    //form.controls['recaptcha'].setValidators([Validators.compose(
    //  [Validators.required]

    //)])


  }
  private validateLength(fieldControl: FormControl, form: NgForm) {
    debugger
    return form.value.length > 7 ? null : {
      InValidLength: true
    };
  }
  private validateAreEqual(fieldControl: FormControl, form: NgForm) {
    debugger
    return fieldControl.value.Password === form.value ? null : {
      NotEqual: true
    };
  }
  // On submit button click    
  onSubmit(form: NgForm) {
  
    if (form.invalid) {
      return;
    } else {
      this._AuthService.signup(this.loginData).subscribe(

        result => {
          var data: any = { Username: this.loginData.Email, Password: this.loginData.Password }
          this._AuthService.login(data).subscribe(res => {
            
            this._AuthService.GetUserInfo();
          this.errorMessage = "";
            if (this.NewsData != null) {
              if (this.NewsData.Type == 'News') {
                this.newsService.SubscribeNews(this.NewsData.ID).subscribe(n => {
                 
                  this.channelService.GetChannel();
                  this.newsService.SearchNews();
                })


              }
              if (this.NewsData.Type == 'Channel') {
                this.channelService.SubscribeChannel(this.NewsData.ID).subscribe(c => {
               
                  this.channelService.GetChannel();
                  this.newsService.SearchNews();
                })


              }


            } else {
              this.channelService.GetChannel();
              this.newsService.SearchNews();
            }
           
          })
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
