import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from "@angular/router";
import { MaterialDesignModule } from '../material-design/material-design.module';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
@NgModule({
  exports: [
    CommonModule,   
     NavMenuComponent, LoginComponent,
   

  ],
  imports: [
    CommonModule, 
  
    FormsModule,
    ReactiveFormsModule,
 
    MaterialDesignModule,
    RouterModule,
    CommonModule,   
    MaterialDesignModule,
    AngularFontAwesomeModule
  ],
  entryComponents: [ NavMenuComponent, LoginComponent, SignupComponent],
  declarations: [
  
    NavMenuComponent,
  
    LoginComponent,
    SignupComponent
  ],

})
export class SharedModule { }
