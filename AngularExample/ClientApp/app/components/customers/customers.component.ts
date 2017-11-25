import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { IPaginatedView, IItemsWithPagination, IPagerModel } from "../../shared/pagination";

interface ICustomer {
    name: string;
    birthDate: number;
    gender: number;
}

@Component({
    selector: 'customers',
    templateUrl: './customers.component.html'
})
export class CustomersList implements IPaginatedView<ICustomer> {

    public itemsWithPagination: IItemsWithPagination<ICustomer>;
    private pagerModel: IPagerModel;
    
    get pagesTotal(): number {
        return !this.pagerModel ? 0 : this.pagerModel.pages;
    }

    get items(): ICustomer[] {
        return !this.itemsWithPagination ? [] : this.itemsWithPagination.items;
    }

    private ps: number = 20;
    private p: number = 1;
    private c: string = 'Id';

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + `api/customers?pageSize=${this.ps}&page=${this.p}&orderby=${this.c}`)
            .subscribe(result => {
                this.itemsWithPagination = result.json();
            }, error => console.error(error));

        http.get(baseUrl + `api/customers/pagination?pageSize=${this.ps}&page=${this.p}&orderby=${this.c}`)
            .subscribe(result => {
                this.pagerModel = result.json();
            }, error => console.error(error));
    }
}
