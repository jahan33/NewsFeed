import { Injectable, Inject } from '@angular/core';
import { HttpRequest, HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { LoginService } from '../auth/login.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  public baseURL: string = "";
  public newsList: any = [];
  public filter: any = {}
  public totalNews: number = 0;
  public errorMessage: any = {}
  constructor(private toastr: ToastrService,private http: HttpClient, private router: Router, private route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string, public authService: AuthService) {    
    this.baseURL = baseUrl;
    this.filter.index = 0;
    this.filter.pageSize = 6;
    this.authService.GetUserInfo();
  }
  // View News Item
  public ViewNewsItem(PKNews: number = 0) {
    return this.http.get(this.baseURL + 'api/News/GetNews?PKNews=' + PKNews).map(result => {
      return result;
    })
  }
   // Search News 
  public SearchNews() {
    if (this.filter.Search != null && this.filter.Search != undefined && this.filter.Search.length > 0) {
      this.filter.showFilter = true;
    }
    if (this.authService.CurrentUser.PKUser != null && this.authService.CurrentUser.PKUser != undefined) {
      this.filter.PKUser = this.authService.CurrentUser.PKUser;

    } else {
      this.filter.PKUser = 0;

    }
     return this.http.post(this.baseURL + 'api/News/GetNews', this.filter).subscribe(response => {
      
      let result: any = response;
      this.newsList = result[0].NewsList
      this.totalNews = result[0].Total;
      return result;
    })
  }
  // Subscribe News
  public SubscribeNews(FKNews: any = {}) {
    
    let model: any = {};
    model.FKNews = FKNews;
    model.FKUser=this.authService.CurrentUser.PKUser
    return this.http.post(this.baseURL + 'api/News/SubscribeNews', model).map(response => {
      let result: any = response;
      if (result.IsActive == true) {
        this.toastr.success("News bookmark successfully");
      } else {
        this.toastr.warning("Bookmark removed successfully");
      }
      this.SearchNews();
      return result;
    })
  }
}
