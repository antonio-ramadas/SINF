import { Component, Input } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Service } from './../../app.service';

@Component({
  moduleId: module.id,
  selector: 'doughnut-chart-demo',
  templateUrl: 'graph-test.html',
  styles: [`
    canvas {
      
    }
    `]
})

export class DoughnutChartDemoComponent {
  @Input() method;
  @Input() idSalesRep;
  errorMessage = "";

  constructor(private route: ActivatedRoute, private service: Service) {
  }

  ngAfterViewInit() {
    switch(this.method) {
      case 0:
         this.topProductsChart(this.idSalesRep);
         break;
    }  
  }

  // Doughnut
  public doughnutChartLabels:string[] = ['Download Sales', 'In-Store Sales', 'Mail-Order Sales'];
  public doughnutChartData:number[] = [350, 450, 100];
  public doughnutChartType:string = 'doughnut';

  // events
  public chartClicked(e:any):void {
    //console.log(e);
  }

  public chartHovered(e:any):void {
    //console.log(e);
  }

  topProductsChart(id: string): void {
    this.service.getTopCategoriesBySalesRepresentative(id)
          .subscribe(
          chart => { this.parseTopProducts(chart); },
          error => this.errorMessage = <any>error);
  }

  private parseTopProducts(chart: any[]):void {
    let chartLabels = [];
    let chartData = [];
    for (let categories of chart) {
      chartLabels.push(categories.category.description);
      chartData.push(categories.numSales);
    }
    this.doughnutChartLabels = chartLabels;
    this.doughnutChartData = chartData;
  }
}
