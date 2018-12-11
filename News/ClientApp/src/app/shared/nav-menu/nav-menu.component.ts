import { Component } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { NewsService } from '../../services/news/news.service';
import { LoginService } from '../../services/auth/login.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public strNull: any = null;
  constructor(public authService: AuthService, public newsService: NewsService, public loginService: LoginService) {
    this.authService.GetUserInfo();
  }
  public toggleMenu() {
   
    
    this.authService.sidebarStatus = !this.authService.sidebarStatus
  }
  public Logout() {
    this.authService.logout();
    this.newsService.SearchNews();
  }
}
