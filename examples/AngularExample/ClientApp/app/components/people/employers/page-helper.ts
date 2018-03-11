import { IPage } from "shared/pagination";

export class PageHelper {

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