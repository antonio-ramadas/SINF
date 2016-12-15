import { Component, Input } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Service } from './../../app.service';

@Component({
  moduleId: module.id,
  selector: 'bar-chart-demo',
  templateUrl: 'graph-test3.html',
  providers: [Service]
})

export class BarChartDemoComponent {
  @Input() method;
  @Input() idSalesRep;
  errorMessage = "";

  constructor(private route: ActivatedRoute, private service: Service) {
  }

  ngAfterViewInit() {
    switch(this.method) {
      case 0:
         this.salesFlowChart(this.idSalesRep);
         break;
      case 1:
         this.salesFlowChart(this.idSalesRep);
         //this.salesFlowChartManager();
         break;
    }  
  }

  public barChartOptions:any = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  public barChartLabels:string[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
  public barChartType:string = 'bar';
  public barChartLegend:boolean = true;

  public barChartData:any[] = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
    {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'},
    {data: [28, 48, 40, 19, 86, 270, 0], label: 'Series C'}
  ];

  salesFlowChartManager():void {
    this.barChartLabels =  ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    let salesFlow;
    this.service.getIncomeBySalesRepresentativeManager("2016")
      .subscribe(
      chart => { this.parseSalesFlow(chart); },
      error => this.errorMessage = <any>error);
  }



  salesFlowChart(id: string):void {
    this.barChartLabels =  ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    let salesFlow;
    this.service.getIncomeBySalesRepresentative(id)
      .subscribe(
      chart => { this.parseSalesFlow(chart); },
      error => this.errorMessage = <any>error);
  }

  private parseSalesFlow(chart: any[]):void {
    let barData = [];
    for (let year of chart) {
      let arr = [];
      for (let month of year.sales) {
        arr.push(month.income);
      }
      barData.push({data: arr, label: year.year});
    }
    this.barChartData = barData;
  }

  // events
  public chartClicked(e:any):void {
    //console.log(e);
  }

  public chartHovered(e:any):void {
    //console.log(e);
  }

  public randomize():void {
    // Only Change 3 values
    let data = [
      Math.round(Math.random() * 100),
      59,
      80,
      (Math.random() * 100),
      56,
      (Math.random() * 100),
      40];
    let clone = JSON.parse(JSON.stringify(this.barChartData));
    clone[0].data = data;
    this.barChartData = clone;
    /**
     * (My guess), for Angular to recognize the change in the dataset
     * it has to change the dataset variable directly,
     * so one way around it, is to clone the data, change it and then
     * assign it;
     */
  }
}
