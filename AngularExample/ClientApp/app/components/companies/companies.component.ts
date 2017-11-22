import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'companies',
    templateUrl: './companies.component.html'
})
export class CompaniesComponent {

    public companies: Company[];

    private pageSize: number = 20;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + `api/companies?pageSize=${this.pageSize}&page=${1}&orderby=${'Id'}`)
            .subscribe(result => {
                this.companies = result.json() as Company[];
            }, error => console.error(error));
    }
}

interface Company {
    name: string;
    birth: number;
    gender: number;
}
