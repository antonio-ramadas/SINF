export class CartLine {
    costPrice: string;
    description: string;
    productID: string;
    quantity: number;
    sellingPrice: string;
    active: boolean;

    constructor(wishlist: JSON) {
        this.active = false;
        this.costPrice = wishlist['costPrice'];
        this.description = wishlist['description'];
        this.productID = wishlist['productId'];
        this.quantity = Number(wishlist['quantity']);
        this.sellingPrice = wishlist['sellingPrice'].replace(',', '.');
    }
}