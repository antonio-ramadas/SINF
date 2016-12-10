import { Component } from '@angular/core';

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
  // Doughnut
  public doughnutChartLabels:string[] = ['Download Sales', 'In-Store Sales', 'Mail-Order Sales'];
  public doughnutChartData:number[] = [350, 450, 100];
  public doughnutChartType:string = 'doughnut';

  // events
  public chartClicked(e:any):void {
    console.log(e);
  }

  public chartHovered(e:any):void {
    console.log(e);
  }
}
