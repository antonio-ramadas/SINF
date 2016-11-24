export class Product {
  id: string;
  price: string;
  description: string;
  salesCount: string;
  stock: string;
  availability: boolean;
  imageUrls: string[];
  family: string;
  vat: string;
  warehouses: string[];

  constructor(product: JSON) {
    this.id = product['id'];
    this.price = product['price'];
    this.description = product['description'];
    this.salesCount = product['salesCount'];
    this.stock = product['quantity'];
    this.imageUrls = product['imageUrls'];
    this.family = product['family'];
    this.vat = product['vat'];
    this.warehouses = product['warehouses'];
  }
}