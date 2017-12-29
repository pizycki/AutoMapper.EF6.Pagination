import { IPage } from '../../shared/pagination';
import { Component, Inject, HostListener } from "@angular/core";
import { Http } from "@angular/http";
import { CustomersService } from "../../services/customers.service";
import { Observable } from "rxjs/Observable";
import { OnInit } from "@angular/core/src/metadata/lifecycle_hooks";

interface ICustomer {
    id: string;
    name: string;
    birthDate: number;
    gender: number;
}

@Component({
    selector: "customers-infinite",
    templateUrl: "./customers-infinite.component.html",
    providers: [CustomersService]
})
export class CustomersInfinite {

    page: IPage<ICustomer>;
    private orderBy = "Id";

    get items(): ICustomer[] {
        return !!this.page ? this.page.items : [];
    }

    ngOnInit(): void {
        this.page = {
            number: 1,
            size: 20,
            items: [],
            pagesTotal: 0
        }

        this.loadPage(1, true);
    }

    @HostListener("window:scroll", ["$event"])
    onScroll(): void {
        if (this.shouldLoadData()) {
            this.loadPage(this.page.number + 1);
        }
    }

    private loadPage(page: number = 1, totalPages: boolean = false): void {
        this.customersService
            .customersPaginated(page, this.page.size, this.orderBy, totalPages)
            .subscribe(result => {
                let page = result.json();
                page.items = this.page.items.concat(page.items);
                this.page = page;
            }, error => console.error(error));
    }

    private shouldLoadData = (): boolean =>
        this.page.number !== this.page.pagesTotal
        && (window.innerHeight + window.scrollY) >= document.body.offsetHeight;

    constructor(private customersService: CustomersService) {
    }
}