import { Component, Inject, HostListener } from "@angular/core";
import { Http } from "@angular/http";
//import { IPaginatedView, IPage, IPagerModel } from "../../shared/pagination";
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
export class CustomersInfinite  {

    // private page: number = 0;
    // private pageSize: number = 50;
    // private columnToOrderBy: string = "Id";
    // private thereIsMore: boolean = true; // stay positive!

    // public page: IPage<ICustomer>;

    // get items(): ICustomer[] {
    //     return !this.page ? [] : this.page.items;
    // }

    // ngOnInit(): void {
    //     if (this.shouldLoadData()) {
    //         this.loadData(++this.page);
    //     }
    // }

    // @HostListener("window:scroll", ["$event"])
    // onScroll(): void {
    //     if (this.shouldLoadData()) {
    //         this.loadData(++this.page);
    //     }
    // }

    // private loadData(page: number = 1): void {
    //     this.customersService
    //         .customersPaginated(page, this.pageSize, this.columnToOrderBy)
    //         .subscribe(result => {
    //             let newData: IPage<ICustomer> = result.json();

    //             if (newData.items.length === 0) {
    //                 this.thereIsMore = false;
    //                 return;
    //             }

    //             this.page = {
    //                 page: newData.page,
    //                 pageSize: newData.pageSize,
    //                 items: this.page !== undefined
    //                     ? this.page.items.concat(newData.items)
    //                     : newData.items
    //             };
    //         }, error => console.error(error));
    // }

    // private shouldLoadData = (): boolean => this.thereIsMore && (window.innerHeight + window.scrollY) >= document.body.offsetHeight;

    // constructor(private customersService: CustomersService) {
    // }
}