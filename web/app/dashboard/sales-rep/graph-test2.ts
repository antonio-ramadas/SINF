import { Component, Input } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Service } from './../../app.service';

@Component({
  moduleId: module.id,
  selector: 'line-chart-demo',
  templateUrl: 'graph-test2.html'
})
export class LineChartDemoComponent {
  @Input() method;
  @Input() idSalesRep;
  errorMessage = "";

  constructor(private route: ActivatedRoute, private service: Service) {
  }

  ngAfterViewInit() {
    switch(this.method) {
      case 0:
         this.incomeChart(this.idSalesRep);
         break;
    }  
  }

  incomeChart(id: string):void {
    this.lineChartLabels =  ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    this.service.getIncomeBySalesRepresentativeByYear(id, '2016')
      .subscribe(
      chart => { this.getIncomePerSales(id, chart); },
      error => this.errorMessage = <any>error);
  }

  private getIncomePerSales(id: string, incomeChart: any[]):void {
    this.service.getIncomePerSaleBySalesRepresentativeByYear(id, '2016')
      .subscribe(
      chart => { this.parseIncome(incomeChart, chart); },
      error => this.errorMessage = <any>error);
  }

  private parseIncome(incomeChart: any[], incomePerSalesChart: any[]):void {
    let chartData = [];
    for (let month of incomeChart['sales']) {
      chartData.push(month.income);
    }
    this.lineChartData = [{data: chartData, label: 'Income'}];

    let chartDataPerSale = [];
    for (let month of incomePerSalesChart['monthRates']) {
      chartDataPerSale.push(month.incomePerMonth);
    }
    this.lineChartData.push({data: chartDataPerSale, label: 'Income Per Sale'});
  }

  // lineChart
  public lineChartData:Array<any> = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'}
  ];
  public lineChartLabels:Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
  public lineChartOptions:any = {
    animation: false,
    responsive: true
  };
  public lineChartColors:Array<any> = [
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // dark grey
      backgroundColor: 'rgba(77,83,96,0.2)',
      borderColor: 'rgba(77,83,96,1)',
      pointBackgroundColor: 'rgba(77,83,96,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(77,83,96,1)'
    },
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];
  public lineChartLegend:boolean = true;
  public lineChartType:string = 'line';

  public randomize():void {
    let _lineChartData:Array<any> = new Array(this.lineChartData.length);
    for (let i = 0; i < this.lineChartData.length; i++) {
      _lineChartData[i] = {data: new Array(this.lineChartData[i].data.length), label: this.lineChartData[i].label};
      for (let j = 0; j < this.lineChartData[i].data.length; j++) {
        _lineChartData[i].data[j] = Math.floor((Math.random() * 100) + 1);
      }
    }
    this.lineChartData = _lineChartData;
  }

  // events
  public chartClicked(e:any):void {
    //console.log(e);
  }

  public chartHovered(e:any):void {
    //console.log(e);
  }
}
