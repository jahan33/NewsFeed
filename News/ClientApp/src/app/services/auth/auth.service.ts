
   
import { Injectable, Inject } from '@angular/core';
import { HttpRequest, HttpClient } from '@angular/common/http';
import { Router} from '@angular/router';

//import * as decode from 'jwt-decode';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { error } from 'protractor';
import { ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Injectable()
export class AuthService {
  public sidebarStatus: any ='opened'
   
  public CurrentUser: any = {}   
  public currentPageTitle: any = {};
  public baseURL: string = "";
  public errorMessage: string = "";
  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, public title: Title, @Inject('BASE_URL') baseUrl: string) {
    this.CurrentUser = {};
    this.CurrentUser.FullName = "";
    this.GetUserInfo();
    this.baseURL = baseUrl;
    }
   
  public showPageTitle(path: string = "", title: string = "") {
    
    this.currentPageTitle = title;
  }
  public GetUserInfo(): {} {
    
        try {
            var userInfo = localStorage.getItem('userInfo');
            if (userInfo != null) {
          
              let userInfoList = JSON.parse(userInfo);
              this.CurrentUser = userInfoList.User.Users;
               
              
                return userInfoList.User.Users
            } else {
              
                return null;
            }
        } catch (ex) {}
  }
  
    public static getToken(): string {
     
        var userInfo = localStorage.getItem('userInfo');
        if (userInfo != null) {
            let userInfoList = JSON.parse(userInfo);
            return userInfoList.token;
        } else {

            return "";
        }
    }
    public get getUser() {
    
        var userInfo = localStorage.getItem('userInfo');
        if (userInfo != null) {
          let userInfoList = JSON.parse(userInfo);
          
            return userInfoList.User.Users;
        } else {

            return "";
        }
    }

    public  getToken(): string {
       
        var userInfo = localStorage.getItem('userInfo');
        if (userInfo != null) {
            let userInfoList = JSON.parse(userInfo);
            return userInfoList.token;
        } else {

            return "";
        }
    }
    public isAuthenticated(): boolean {
        // get the token
        const token = this.getToken();
        if (token != null && token != undefined && token.length > 0) {
            return true;
        } else {
            return false;
        }
       
  }
  // Set Menu Access
  public HaveAccess(name): boolean {
  
   
    if (name == "SKIP") {
    //  this.SetTitle("Home");
          return true;

    }
 //   this.SetTitle(name);

        var userInfo = localStorage.getItem('userInfo');
       
                
        if (userInfo != null) {
            let userInfoList = JSON.parse(userInfo);

            let appNameList: any = {}
                var menu = userInfoList.Menu;
            var isValid = menu.filter((item) => item.Application_URL === name);
            if (isValid != null && isValid.length > 0) {
                return true;
            } else {

                return false;
            }
        } else {
            return false;
        }
    }
  permission: boolean;
  //Set Page Title
  public moduleName: string = "News";
  public SetTitle(pagename:string='') {
    let title: any = this.moduleName + "-" + pagename;
    this.title.setTitle(title)
  }

       
       
  ///Login  
    login(loginData: any = {}) {
        this.logout();
         
       
        let clinetID: string = "NewsAPI";
        loginData.type = "";
        loginData.appName = "News"
       
        let data: string = "grant_type=password&username=" + loginData.Username + "&password=" + loginData.Password ;


        let scope1: any = [];

        scope1.push(loginData.type);
        scope1.push(loginData.appName)


        data = data + "&client_id=" + clinetID + '&scope=' + scope1

        // let data: any = { "grant_type": "password", "username": loginData.userName, "password": loginData.password, "client_id": this.config.clientId };
        console.log(data)
        return this.http.post(this.baseURL + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).map((response: Response) => {
          localStorage.setItem("userInfo", JSON.stringify(response));
       
            this.GetUserInfo();
           
            return response
        }, msg => {
       
          return msg;
          });

  }
  ///logout  
  signup(tblUser: any = {}) {
    this.logout();
    


    return this.http.post(this.baseURL + 'api/user/signup', tblUser).map((response: Response) => {
      
    

        
    }, msg => {
      
      return msg;
    });

  }
  // Logout
  logout() {
    this.CurrentUser = {};
    this.CurrentUser.FullName = "";
    
    localStorage.setItem('userInfo',"");
      localStorage.clear();
     

  }






}

@Injectable()
export class WindowRef {
    constructor() { }

    getNativeWindow() {
        return window;
    }
}
