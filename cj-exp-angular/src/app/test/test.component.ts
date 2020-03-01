import { Component, OnInit} from '@angular/core';


@Component({ templateUrl: 'test.component.html' })
export class TestComponent implements OnInit {

  loaded: boolean;

  constructor() {
    this.loaded = true;
  }

  ngOnInit() {

  }

}
