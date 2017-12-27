import { Http } from "@angular/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class CustomersService {

    constructor(
        private http: Http,
        @Inject("BASE_URL") private baseUrl: string) {
    }

    customersPaginated = (page: number, size: number, orderBy: string): Observable<any> =>
        this.http.get(this.baseUrl + `api/customers?pageSize=${size}&page=${page}&orderby=${orderBy}`)

    customersPaginatedPager = (size: number, orderBy: string): Observable<any> =>
        this.http.get(this.baseUrl + `api/customers/pagination?pageSize=${size}&page=${2}&orderby=${orderBy}`)
}