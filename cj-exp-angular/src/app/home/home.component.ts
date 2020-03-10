import { Component, OnInit} from '@angular/core';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {

  loaded = false;

  constructor() {
    this.loaded = true;
  }

  ngOnInit() {

  }

}
