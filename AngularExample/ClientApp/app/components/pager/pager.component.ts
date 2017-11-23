import { Component, Input } from "@angular/core";
import { IPagination } from "../../shared/pagination";

@Component({
    selector: 'pager',
    templateUrl: "pager.component.html"
})
export class Pager {
    @Input()
    pagination: IPagination;
    
    @Input()
    pagesTotal: number;
}