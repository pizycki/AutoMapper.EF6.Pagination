import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";
import { IPagination } from "../../shared/pagination";

@Component({
    selector: 'pager',
    templateUrl: "pager.component.html"
})
export class Pager{

    @Input()
    pagination: IPagination;

    @Input()
    pagesTotal: number; 

    @Output()
    pageSelected = new EventEmitter();

    selectPage(page: number): void {
        console.log(page);
        this.pageSelected.emit(page);
    }

    get pages(): number[] {
        if (!this.pagesTotal) {
            return [];
        }

        let ls = [];
        for (let i = 1; i <= this.pagesTotal; i++) {
            ls.push(i);
        }
        return ls;
    }
}