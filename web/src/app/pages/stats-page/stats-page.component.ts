import { Component } from '@angular/core';
import { StatisticComponent } from '../../components/statistic/statistic.component';

@Component({
  selector: 'app-stats-page',
  standalone: true,
  imports: [StatisticComponent],
  templateUrl: './stats-page.component.html',
  styleUrl: './stats-page.component.css'
})
export class StatsPageComponent {

}
