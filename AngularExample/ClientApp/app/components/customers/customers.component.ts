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

    private http: Http;
    private baseUrl: string;
    private pagerModel: IPagerModel;

    public itemsWithPagination: IItemsWithPagination<ICustomer>;

    get pagesTotal(): number {
        return !this.pagerModel ? 0 : this.pagerModel.pages;
    }

    get items(): ICustomer[] {
        return !this.itemsWithPagination ? [] : this.itemsWithPagination.items;
    }

    public onPageSelected(page: number): void {
        this.loadData(page);
    }

    private loadData(page: number = 1): void {
        this.http.get(this.baseUrl + `api/customers?pageSize=${this.ps}&page=${page}&orderby=${this.c}`)
            .subscribe(result => {
                this.itemsWithPagination = result.json();
            }, error => console.error(error));
    }

    private loadPagination(): void {
        this.http.get(this.baseUrl + `api/customers/pagination?pageSize=${this.ps}&page=${1}&orderby=${this.c}`)
            .subscribe(result => {
                this.pagerModel = result.json();
            }, error => console.error(error));
    }

    private ps: number = 20;
    private c: string = 'Id';

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {

        this.http = http;
        this.baseUrl = baseUrl;

        this.loadData();
        this.loadPagination();

    }
}
