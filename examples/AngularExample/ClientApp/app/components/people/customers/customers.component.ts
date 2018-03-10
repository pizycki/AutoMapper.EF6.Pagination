import { Component, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { OnInit } from "@angular/core/src/metadata/lifecycle_hooks";
import { IPage } from "shared/pagination";
import { PeopleProvider } from "providers/people.provider";
import { IPerson } from "components/people/models/person";

@Component({
    selector: "customers",
    templateUrl: "./customers.component.html",
    providers: [PeopleProvider]
})
export class CustomersComponent implements OnInit {

    private pageSize: number = 10;
    private columnToOrderBy: string = "Id";

    page: IPage<IPerson>;

    get pageNumber(): number {
        return !!this.page ? this.page.number : 1;
    }

    get items(): IPerson[] {
        return !!this.page ? this.page.items : [];
    }

    get pagesTotal(): number {
        return !!this.page ? this.page.pagesTotal : 0;
    }

    public onPageSelected(page: number): void {
        this.loadPage(page);
    }

    private loadPage(page: number = 1, totalPages: boolean = false): void {
        this.PeopleProvider
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

    constructor(private PeopleProvider: PeopleProvider) {
    }
}