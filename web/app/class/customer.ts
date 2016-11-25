export class Customer {
  id: string;
  phone: string;
  name: string;
  email: string;
  address: string;
  salesPersonName: string;
  salesPersonId: string;
  nationality: string;
  dateOfBirth: string;
  gender: string;
  nif: string;

  constructor(data: JSON) {
    this.phone = data['phoneNumber'];
    this.name = data['name'];
    this.id = data['id'];
    this.email = data['email'];
    this.address = data['address'];
    this.salesPersonName = data['salesPersonName'];
    this.salesPersonId = data['salesPersonId'];
    this.nationality = data['nationality'];
    this.dateOfBirth = data['dateOfBirth'];
    this.gender = data['gender'];
    this.nif = data['nif'];
  }
}