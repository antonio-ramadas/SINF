export class Product {
  id: string;
  price: string;
  description: string;
  salesCount: string;
  stock: string;
  availability: boolean;
  imageUrl: string;
  category: string;
  subCategory: string;
  vat: string;
  warehouses: string[];

  constructor(product: JSON) {
    this.id = product['id'];
    this.price = product['price'];
    this.description = product['description'];
    this.salesCount = product['salesCount'];
    this.stock = product['quantity'];
    this.imageUrl = product['imageURL'] || 'default.jpg';
    this.category = product['category'];
    this.subCategory = product['subCategory'];
    this.vat = product['vat'];
    this.warehouses = product['warehouses'];
  }

  isSimilar(hint: string, category: string) : boolean {
    let sameCategory = true;
    if (category != null && category != '') {
      sameCategory = category == this.category || category == this.subCategory;
    }

    return sameCategory && this.description.toLowerCase().indexOf(hint) >= 0;
  }
}