import { Http } from "@angular/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class CustomersService {

    constructor(
        private http: Http,
        @Inject("BASE_URL") private baseUrl: string) {
    }

    customersPaginated(
        page: number, size: number, totalPages: boolean = false,
        orderBy: string = ""): Observable<any> {
        let query = `api/customers?size=${size}&number=${page}`;
        if (!!orderBy && orderBy !== "") query + `&orderby=${orderBy}`;
        if (totalPages) query + "&includeTotalPages";
        return this.http.get(this.baseUrl + query);
    }
}