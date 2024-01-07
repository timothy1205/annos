import { StatsService } from './../../services/stats.service';
import { Component, OnInit } from '@angular/core';
import { StatisticComponent } from '../../components/statistic/statistic.component';
import { StatsModel } from '../../models/stats.model';
import { NgIf } from '@angular/common';
import { NgApexchartsModule } from 'ng-apexcharts';

@Component({
  selector: 'app-stats-page',
  standalone: true,
  imports: [StatisticComponent, NgIf, NgApexchartsModule],
  templateUrl: './stats-page.component.html',
  styleUrl: './stats-page.component.css',
})
export class StatsPageComponent implements OnInit {
  constructor(protected statsService: StatsService) {}

  stats: StatsModel | null = null;
  series: number[] = [];
  labels: string[] = [];

  async ngOnInit() {
    this.stats = await this.statsService.getStatistics();
    this.series = this.stats.category_splits.map((split) => split.cost);
    this.labels = this.stats.category_splits.map((split) => split.name);
  }
}
