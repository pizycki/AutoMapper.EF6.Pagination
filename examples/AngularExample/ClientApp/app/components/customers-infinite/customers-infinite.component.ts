import { Component, Inject, HostListener } from "@angular/core";
import { Http } from "@angular/http";
import { IPaginatedView, IItemsWithPagination, IPagerModel } from "../../shared/pagination";
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
export class CustomersInfinite implements IPaginatedView<ICustomer>, OnInit {

    private page: number = 0;
    private pageSize: number = 50;
    private columnToOrderBy: string = "Id";
    private thereIsMore: boolean = true; // stay positive!

    public itemsWithPagination: IItemsWithPagination<ICustomer>;

    get items(): ICustomer[] {
        return !this.itemsWithPagination ? [] : this.itemsWithPagination.items;
    }

    ngOnInit(): void {
        if (this.shouldLoadData()) {
            this.loadData(++this.page);
        }
    }

    @HostListener("window:scroll", ["$event"])
    onScroll(): void {
        if (this.shouldLoadData()) {
            this.loadData(++this.page);
        }
    }

    private loadData(page: number = 1): void {
        this.customersService
            .customersPaginated(page, this.pageSize, this.columnToOrderBy)
            .subscribe(result => {
                let newData: IItemsWithPagination<ICustomer> = result.json();

                if (newData.items.length === 0) {
                    this.thereIsMore = false;
                    return;
                }

                this.itemsWithPagination = {
                    page: newData.page,
                    pageSize: newData.pageSize,
                    items: this.itemsWithPagination !== undefined
                        ? this.itemsWithPagination.items.concat(newData.items)
                        : newData.items
                };
            }, error => console.error(error));
    }

    private shouldLoadData = (): boolean => this.thereIsMore && (window.innerHeight + window.scrollY) >= document.body.offsetHeight;

    constructor(private customersService: CustomersService) {
    }
}