import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'customers',
    templateUrl: './customers.component.html'
})
export class CustomersList implements IPaginatedView<ICustomer> {

    public itemsWithPagination: IItemsWithPagination<ICustomer>;

    get items(): ICustomer[] {
        return !this.itemsWithPagination ? [] : this.itemsWithPagination.items;
    }
    
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + `api/customers?pageSize=${20}&page=${1}&orderby=${'Id'}`)
            .subscribe(result => {
                this.itemsWithPagination = result.json();
            }, error => console.error(error));
    }
}

interface IPaginatedView<T> {
    itemsWithPagination: IItemsWithPagination<T>;
    items: T[];
}

interface IItemsWithPagination<T> extends IPagination {
    items: T[];
}

interface IPagination {
    page: number;
    pageSize: number;
}

interface ICustomer {
    name: string;
    birth: number;
    gender: number;
}