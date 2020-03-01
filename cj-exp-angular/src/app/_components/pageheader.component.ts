import { Component, OnInit, Input } from '@angular/core';

@Component({ templateUrl: 'pageheader.component.html', selector: 'page-header' })
export class PageHeaderComponent implements OnInit {
    @Input()
    pageTitle: string;

    ngOnInit() {

    }


}