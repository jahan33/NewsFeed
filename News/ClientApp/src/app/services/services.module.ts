import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService, WindowRef } from './auth/auth.service';
import { NewsService } from './news/news.service';
import { ChannelService } from './channel/channel.service';
import { LoginService } from './auth/login.service';

@NgModule({
  imports: [
    CommonModule
  ],
  providers: [WindowRef, AuthService, NewsService, ChannelService, LoginService],
  declarations: []
})
export class ServicesModule { }
