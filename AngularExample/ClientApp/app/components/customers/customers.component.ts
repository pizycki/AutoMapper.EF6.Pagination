import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'customers',
    templateUrl: './customers.component.html'
})
export class CustomersList {

    public customers: Customer[];

    private pageSize: number = 20;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + `api/customers?pageSize=${this.pageSize}&page=${1}&orderby=${'Id'}`)
            .subscribe(result => {
                this.customers = result.json() as Customer[];
            }, error => console.error(error));
    }
}

interface Customer {
    name: string;
    birth: number;
    gender: number;
}