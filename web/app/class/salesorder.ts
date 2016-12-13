import { SalesOrderLine } from './salesorderline';

export class SalesOrder {
  id: string;
  date: Date;
  entity: string;
  numDoc: string;
  salesRep: string;
  serie: string;
  total: string;
  address: string;
  lines: SalesOrderLine[] = [];

  constructor(data: JSON) {
    this.id = data['id'];
    this.date = new Date(data['date']);
    this.entity = data['entity'];
    this.numDoc = data['numDoc'];
    this.salesRep = data['salesRep'];
    this.serie = data['serie'];
    this.total = data['totalMerc'];
    this.address = data['address'];
    if (data['LinhasDoc'] != null)
      for (let line of data['LinhasDoc'])
        this.lines.push(new SalesOrderLine(line));
  }
}