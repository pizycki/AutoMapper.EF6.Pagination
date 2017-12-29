import { Http } from "@angular/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class CustomersService {

    constructor(
        private http: Http,
        @Inject("BASE_URL") private baseUrl: string) {
    }

    customersPaginated = (page: number, size: number, orderBy: string, totalPages: boolean = false): Observable<any> =>
        this.http.get(this.baseUrl + `api/customers?size=${size}&number=${page}&orderby=${orderBy}&${totalPages ? "includeTotalPages" : ""}`)
}