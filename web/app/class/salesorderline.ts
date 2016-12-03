export class SalesOrderLine {
  codArtigo: string;
  descArtigo: string;
  desconto: string;
  idCabecDoc: string;
  precoUnitario: string;
  quantidade: string;
  totalILiquido: string;
  totalLiquido: string;
  unidade: string;

  constructor(data: JSON) {
    this.codArtigo = data['CodArtigo'];
    this.descArtigo = data['DescArtigo'];
    this.desconto = data['Desconto'];
    this.idCabecDoc = data['IdCabecDoc'];
    this.precoUnitario = data['PrecoUnitario'];
    this.quantidade = data['Quantidade'];
    this.totalILiquido = data['TotalILiquido'];
    this.totalLiquido = data['TotalLiquido'];
    this.unidade = data['Unidade'];
  }
}