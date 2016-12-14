import { CartLine } from './cartline';

export class Cart {
    id: string;
    creationDate: string;
    customerID: string;
    description: string;
    expirationDate: string;
    salesRepID: boolean;
    summary: string;
    type: string;
    value: string;
    lines: CartLine[] = [];

    constructor(wishlist: JSON) {
        this.id = wishlist['id'];
        this.description = wishlist['description'];
        this.creationDate = wishlist['creationDate'];
        this.customerID = wishlist['customerID'];
        this.expirationDate = wishlist['expirationDate'];
        this.salesRepID = wishlist['salesRepID'];
        this.summary = wishlist['summary'];
        this.type = wishlist['type'];
        this.value = wishlist['value'];
        if (wishlist['lines'] != null)
            for (let wish of wishlist['lines'])
                this.lines.push(new CartLine(wish));
    }
}