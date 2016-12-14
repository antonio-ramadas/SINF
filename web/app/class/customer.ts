export class Customer {
  id: string;
  phone: string;
  name: string;
  email: string;
  address: string;
  nationality: string;
  dateOfBirth: string;
  gender: string;
  nif: string;
  notes: string;
  label1: string;
  label2: string;
  label3: string;

  constructor(data: JSON) {
    this.phone = data['phoneNumber'];
    this.name = data['name'];
    this.id = data['id'];
    this.email = data['email'];
    this.address = data['address'];
    this.nationality = data['nationality'];
    this.dateOfBirth = data['dateOfBirth'];
    this.nif = data['nif'];
    this.notes = data['notes'];
    if (data['labels'] != null) {
      this.label1 = data['labels'][0] || "";
      this.label2 = data['labels'][1] || "";
      this.label3 = data['labels'][2] || "";
    }

  }
}