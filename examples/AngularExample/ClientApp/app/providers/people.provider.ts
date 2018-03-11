import { Http } from "@angular/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class PeopleProvider {

    constructor(
        private http: Http,
        @Inject("BASE_URL") private baseUrl: string) {
    }

    getCustomers(page: number, size: number, totalPages: boolean = false): Observable<any> {
        let query = `api/customers?size=${size}&number=${page}`;
        if (totalPages)
            query + "&includeTotalPages";
        return this.http.get(this.baseUrl + query);
    }

    /**
     * @desc Gets next part of infinite page.
     */
    getDirectors = (part: number, size: number): Observable<any> =>
        this.http.get(this.baseUrl + `api/directors?size=${size}&number=${part}`);

    getEmployers(page: number, size: number, totalPages: boolean = false, orderBy: string, desc: boolean = false): Observable<any> {
        let query = `api/employers?size=${size}&number=${page}`;
        if (!!orderBy && orderBy !== "") query + `&orderBy=${orderBy}`;
        if (totalPages) query + "&includeTotalPages";
        console.log("query", query);
        return this.http.get(this.baseUrl + query);
    }
}