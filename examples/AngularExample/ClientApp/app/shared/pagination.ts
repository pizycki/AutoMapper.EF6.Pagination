export interface IPaginatedView<T> {
    itemsWithPagination: IItemsWithPagination<T>;
    items: T[];
}

export interface IItemsWithPagination<T> extends IPagination {
    items: T[];
}

export interface IPagination {
    page: number;
    pageSize: number;
}

export interface IPagerModel {
    pages: number;
}