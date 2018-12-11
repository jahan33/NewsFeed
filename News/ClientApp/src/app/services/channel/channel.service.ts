import { Injectable, Inject } from '@angular/core';
import { HttpRequest, HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ChannelService {

  public baseURL: string = "";
  public channelList: any = [];
  public allChannel: any = [];
  public filter: any = {}
  public totalChannel: number = 0;
  constructor(private toastr: ToastrService,private http: HttpClient, private router: Router, private route: ActivatedRoute, @Inject('BASE_URL') baseUrl: string,
    public authService: AuthService) {
    this.authService.GetUserInfo();
    this.baseURL = baseUrl;
    this.filter.index = 0;
    this.filter.pageSize = 5;
  }
  // View Channel Item
  public ViewChannelItem(PKChannel: number = 0) {
    return this.http.get(this.baseURL + 'api/Channel/GetChannel?PKChannel=' + PKChannel).map(result => {
      return result;
    })
  }
  // Search Channel 
  public SearchChannel() {
    return this.http.post(this.baseURL + 'api/Channel/GetChannel', this.filter).subscribe(response => {
      
      let result: any = response;
      this.channelList = result[0].ChannelList
      this.totalChannel = result[0].Total;
      return result;
    })
  }
  // Get ALL Channel 
  public GetChannel() {
    let PKUser: number = this.authService.CurrentUser.PKUser
    return this.http.get(this.baseURL + 'api/Channel/GetAllChannel?PKUser=' + PKUser).subscribe(response => {
      
      let result: any = response;
      this.allChannel = result
     
      return result;
    })
  }
  // Subscribe Channel
  public SubscribeChannel(FKChannel: any = {}) {

    let model: any = {};
    model.FKChannel = FKChannel;
    model.FKUser = this.authService.CurrentUser.PKUser
    return this.http.post(this.baseURL + 'api/Channel/SubscribeChannel', model).map(response => {
      let result: any = response;
      if (result.IsActive == true) {
        this.toastr.success("Channel subscribe successfully");
      } else {
        this.toastr.warning("Channel unsubscribe successfully");
      }
      this.GetChannel();
      return result;
    })
  }
}
