import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-news-detail',
  templateUrl: './news-detail.component.html',
  styleUrls: ['./news-detail.component.css']
})
export class NewsDetailComponent implements OnInit {
  public newsItem: any = {}
  constructor(public dialogRef: MatDialogRef<NewsDetailComponent>,@Inject(MAT_DIALOG_DATA) public data: any) {
    
    this.newsItem = data;
  }

  ngOnInit() {
  }

}
