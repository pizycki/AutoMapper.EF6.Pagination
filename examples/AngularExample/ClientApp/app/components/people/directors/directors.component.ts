import { Component, Inject, HostListener } from "@angular/core";
import { Http } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { OnInit } from "@angular/core/src/metadata/lifecycle_hooks";
import { IPage } from "shared/pagination";
import { PeopleProvider } from "providers/people.provider";
import { IPerson } from "components/people/models/person";

@Component({
    selector: "directors",
    templateUrl: "./directors.component.html",
    providers: [PeopleProvider]
})
export class DirectorsComponent {

    page: IPage<IPerson>;
    private orderBy = "Id";

    get items(): IPerson[] {
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
        this.loadPage(this.page.number + 1);
    }

    private loadPage(page: number = 1, totalPages: boolean = false): void {
        if (this.canLoadData()) {
            this.PeopleProvider
                .getDirectors(page, this.page.size)
                .subscribe(result => {
                    let page = result.json();
                    page.items = this.page.items.concat(page.items);
                    this.page = page;
                }, error => console.error(error));
        }
    }

    private canLoadData = (): boolean =>
        this.page.number !== this.page.pagesTotal
        && (window.innerHeight + window.scrollY) >= document.body.offsetHeight;

    constructor(private PeopleProvider: PeopleProvider) {
    }
}