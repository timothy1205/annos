import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-statistic',
  standalone: true,
  imports: [],
  templateUrl: './statistic.component.html',
  styleUrl: './statistic.component.css',
  host: {"class": "container"}
})
export class StatisticComponent {
  @Input() value: string = "";
  @Input() label: string = "";

}
