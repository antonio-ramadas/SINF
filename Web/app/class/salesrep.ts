export class SalesRepresentative {
  id: string;
  phone: string;
  name: string;
  email: string;
  address: string;
  nationality: string;
  dateOfBirth: string;
  gender: string;
  active: boolean;

  constructor(data: JSON) {
    this.phone = data['phoneNumber'];
    this.name = data['name'];
    this.id = data['id'];
    this.email = data['email'];
    this.address = data['address'];
    this.nationality = data['nationality'];
    this.dateOfBirth = data['dateOfBirth'];
    this.gender = data['gender'];
    this.active = data['active'];
  }

  isSimilar(hint: string): boolean {
    return this.name.toLowerCase().indexOf(hint) >= 0;
  }
}