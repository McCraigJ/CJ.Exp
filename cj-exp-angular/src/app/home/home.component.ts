import { Component, OnInit} from '@angular/core';


@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {

  loaded: boolean;

  constructor() {
    this.loaded = true;
  }

  ngOnInit() {

  }

}
