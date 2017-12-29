export interface IPage<T> extends IPagination {
    items: T[];
    pagesTotal: number;
}

export interface IPagination {
    number: number;
    size: number;
}