import { Component, Input, OnChanges, SimpleChanges, Output } from "@angular/core";
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