import { Component, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { IPaginatedView, IItemsWithPagination, IPagerModel } from "../../shared/pagination";
import { CustomersService } from "../../services/customers.service";

interface ICustomer {
    name: string;
    birthDate: number;
    gender: number;
}

@Component({
    selector: "customers-infinite",
    templateUrl: "./customers-infinite.component.html",
    providers: [CustomersService]
})
export class CustomersInfinite implements IPaginatedView<ICustomer> {

    private pagerModel: IPagerModel;
    private pageSize: number = 20;
    private columnToOrderBy: string = "Id";

    public itemsWithPagination: IItemsWithPagination<ICustomer>;

    get pagesTotal(): number {
        return !this.pagerModel ? 0 : this.pagerModel.pages;
    }

    get items(): ICustomer[] {
        return !this.itemsWithPagination ? [] : this.itemsWithPagination.items;
    }

    public onPageSelected(page: number): void {
        this.loadData(page);
    }

    private loadData(page: number = 1): void {
        this.customersService
            .customersPaginated(page, this.pageSize, this.columnToOrderBy)
            .subscribe(result => {
                this.itemsWithPagination = result.json();
            }, error => console.error(error));
    }

    private loadPagination(): void {
        this.customersService
            .customersPaginatedPager(this.pageSize, this.columnToOrderBy)
            .subscribe(result => {
                this.pagerModel = result.json();
            }, error => console.error(error));
    }


    constructor(private customersService: CustomersService) {
        this.loadData();
        this.loadPagination();
    }
}