import { Component, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { OnInit } from "@angular/core/src/metadata/lifecycle_hooks";
import { PeopleProvider } from "providers/people.provider";
import { IPage } from "shared/pagination";
import { IPerson } from "components/people/models/person";

@Component({
    selector: "employers",
    templateUrl: "./employers.component.html",
    providers: [PeopleProvider]
})
export class EmployersComponent implements OnInit {

    private pageSize: number = 10;

    orderBy: string;
    desc: boolean;

    columns: string[] = ["Name", "BirthDate", "Gender"];

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
        if (!this.orderBy)
            return;

        this.PeopleProvider
            .getEmployers(page, this.pageSize, totalPages, this.orderBy, this.desc)
            .subscribe(result => {
                let page = result.json();
                if (!page.pagesTotal) {
                    page.pagesTotal = this.pagesTotal;
                }
                this.page = page;
            },
            error => console.error(error));
    }

    ngOnInit(): void {
        // Default sorting
        this.orderBy = this.columns[0];
        this.desc = false;

        this.loadPage(1, true);
    }

    constructor(private PeopleProvider: PeopleProvider) {
    }
}