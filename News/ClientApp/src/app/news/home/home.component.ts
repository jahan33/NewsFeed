import { Component, OnInit, HostListener } from '@angular/core';
import { NewsService } from '../../services/news/news.service';
import { AuthService } from '../../services/auth/auth.service';
import { ChannelService } from '../../services/channel/channel.service';

import { NewsDetailComponent } from '../news-detail/news-detail.component';
import { MatDialog } from '@angular/material';
import { Overlay } from '@angular/cdk/overlay';
import { LoginService } from '../../services/auth/login.service';
import { type } from 'os';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  //Properties
  public opened: any ='opened'

  public newsList: any = [];
  constructor(public newsService: NewsService, public authService: AuthService, public channelService: ChannelService, public dialog: MatDialog, private overlay: Overlay,public loginService: LoginService) {
    this.ResetSearch();
    this.authService.sidebarStatus = false;
    this.authService.GetUserInfo();
    
  }
  // Reset search
  public ResetSearch() {
    this.newsService.filter = {};
    this.newsService.filter.Search = "";
    this.newsService.filter.filterType = "";
    this.newsService.filter.showFilter = false;
    this.GetNews();
  }
  //Get News
  public GetNews(data:any=null) {
    if (data == null) {
      data = {}
      data.pageSize = 5;
      data.pageIndex = 0;
    }
    this.newsService.filter.pageSize = data.pageSize;
    if (data.pageIndex == undefined) {
      data.pageIndex = 0;
    }

    this.newsService.filter.index = data.pageIndex + 1;
    this.newsService.SearchNews();
  }
  // Search By type
  public searchNewsByType(fkChannel: number = 0, type: string = "", name: string = "") {
    this.newsService.filter.index = 0;
    
    if (type == 'Bookmarks') {
      this.newsService.filter.isSubscribe = true;
    } else {
      this.newsService.filter.fkChannel = fkChannel;
    }
    this.newsService.filter.filterType = type;
    this.newsService.filter.typeName = name;
    this.newsService.SearchNews();
    this.newsService.filter.showFilter = true;
  }

  ngOnInit() {
    this.channelService.GetChannel();
    this.GetNews();
    setTimeout(x => {
      this.authService.sidebarStatus = true;
    }, 2000)
  }
  // view news detail
  public GetNewsDetail(newsData:any) {
    
 
    const scrollStrategy = this.overlay.scrollStrategies.noop();
    const dialogRef = this.dialog.open(NewsDetailComponent, {
      data: newsData,
      autoFocus: true,
      scrollStrategy
    });
       
        dialogRef.afterClosed().subscribe(result => {
         

        });
     


  }
  // Subscribe News
  public SubscribeNews(FKNews: number = 0) {
    if (this.authService.CurrentUser.PKUser>0) {
      this.newsService.SubscribeNews(FKNews).subscribe(response => {
        this.GetNews();
      })
    } else {
      let data: any = { ID: FKNews, Type: 'News'}
      this.loginService.showLoginPage(data );
    }
  }
  // Subscribe Channel
  public SubscribeChannel(FKChannel: number = 0) {
    if (this.authService.CurrentUser.PKUser > 0) {
      this.channelService.SubscribeChannel(FKChannel).subscribe(response => {
        this.GetNews();
        this.channelService.GetChannel();
      })
    } else {
      let data: any = { ID: FKChannel, Type: 'Channel' }
      this.loginService.showLoginPage(data);
    }
  }
  // on resize 
  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.authService.sidebarStatus = false;
    setTimeout(x => {
      this.authService.sidebarStatus = true;
    }, 500)
  }
}
