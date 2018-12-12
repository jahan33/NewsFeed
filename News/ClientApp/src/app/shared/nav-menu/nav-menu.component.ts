import { Component } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { NewsService } from '../../services/news/news.service';
import { LoginService } from '../../services/auth/login.service';
import { ChannelService } from 'src/app/services/channel/channel.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public strNull: any = null;
  constructor(public authService: AuthService, public newsService: NewsService, public channelService: ChannelService, public loginService: LoginService) {
    this.authService.GetUserInfo();
  }
  public toggleMenu() {
   
    
    this.authService.sidebarStatus = !this.authService.sidebarStatus
  }
  public Logout() {
    this.authService.logout();
    this.newsService.filter = {};
    this.newsService.filter.Search = "";
    this.newsService.filter.filterType = "";
    this.newsService.filter.showFilter = false;
    this.channelService.filter = {};
    this.newsService.SearchNews();
    this.channelService.SearchChannel();
  }
}
