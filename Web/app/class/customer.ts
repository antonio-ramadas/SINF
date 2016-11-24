export class Customer {
  phone: number;
  name: string;

  constructor(data: JSON) {
    this.phone = data['phoneNumber'];
    this.name = data['name'];
  }
}