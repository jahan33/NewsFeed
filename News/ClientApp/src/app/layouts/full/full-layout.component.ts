import { Component, OnInit, ElementRef } from '@angular/core';

import { AuthService } from '../../services/auth/auth.service';

var fireRefreshEventOnWindow = function () {
    var evt = document.createEvent("HTMLEvents");
    evt.initEvent('resize', true, false);
    window.dispatchEvent(evt);
};

@Component({
    selector: 'app-full-layout',
    templateUrl: './full-layout.component.html',
    styleUrls: ['./full-layout.component.scss']
})

export class FullLayoutComponent implements OnInit {
   

    private _mobileQueryListener: () => void;
  constructor(private elementRef: ElementRef, public authService: AuthService) {
     
      
    }

    ngOnInit() {
    
      this.authService.SetTitle("Home");
    }

    onClick(event) {
        //initialize window resizer event on sidebar toggle click event
        setTimeout(() => { fireRefreshEventOnWindow() }, 300);
    }
   
}
