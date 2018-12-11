import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewsRoutingModule } from './news-routing.module';
import { HomeComponent } from './home/home.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialDesignModule } from '../material-design/material-design.module';
import { SharedModule } from '../shared/shared.module';
import { NewsDetailComponent } from './news-detail/news-detail.component';


@NgModule({
  imports: [
    CommonModule,
    NgbModule,
 
    FormsModule,
    ReactiveFormsModule,
  
   
    MaterialDesignModule, SharedModule,
    NewsRoutingModule,
   
  ],
  entryComponents: [HomeComponent, NewsDetailComponent],
  declarations: [HomeComponent, NewsDetailComponent]
})
export class NewsModule { }
