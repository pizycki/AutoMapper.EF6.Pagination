import { Component, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { IPage } from "../../shared/pagination";
import { CustomersService } from "../../services/customers.service";
import { OnInit } from "@angular/core/src/metadata/lifecycle_hooks";

interface ICustomer {
    name: string;
    birthDate: number;
    gender: number;
}

class PageHelper {

    static ReplaceItems<T>(ls1: T[], ls2: T[]): any {
        return !!ls2 ? ls2 : ls1;
    }

    static AppendItems<T>(ls1: T[], ls2: T[]): any {
        return ls1.concat(ls2);
    }

    /**
     * Merges two objects of the same type.
     * If property value is not present on second object, it takes value from first type. 
     * @param p1 First object
     * @param p2 Second object
     */
    static merge<T>(p1: IPage<T>, p2: IPage<T>): IPage<T> {
        p2.number = !!p2.number ? p2.number : p1.number;
        p2.size = !!p2.size ? p2.size : p1.size;
        return p2;
    }
}

@Component({
    selector: "customers-fixed",
    templateUrl: "./customers-fixed.component.html",
    providers: [CustomersService]
})
export class CustomersFixed implements OnInit {

    private pageSize: number = 10;

    page: IPage<ICustomer>;

    get pageNumber(): number {
        return !!this.page ? this.page.number : 1;
    }

    get items(): ICustomer[] {
        return !!this.page ? this.page.items : [];
    }

    get pagesTotal(): number {
        return !!this.page ? this.page.pagesTotal : 0;
    }

    public onPageSelected(page: number): void {
        this.loadPage(page);
    }

    private loadPage(page: number = 1, totalPages: boolean = false): void {
        this.customersService
            .customersPaginated(page, this.pageSize, totalPages)
            .subscribe(result => {
                let page = result.json();
                if (!page.pagesTotal) {
                    page.pagesTotal = this.pagesTotal;
                }
                this.page = page;
            }, error => console.error(error));
    }

    ngOnInit(): void {
        this.loadPage(1, true);
    }

    constructor(private customersService: CustomersService) {
    }
}