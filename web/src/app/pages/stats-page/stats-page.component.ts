import { StatsService } from './../../services/stats.service';
import { Component, OnInit } from '@angular/core';
import { StatisticComponent } from '../../components/statistic/statistic.component';
import { StatsModel } from '../../models/stats.model';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-stats-page',
  standalone: true,
  imports: [StatisticComponent, NgIf],
  templateUrl: './stats-page.component.html',
  styleUrl: './stats-page.component.css',
})
export class StatsPageComponent implements OnInit {
  constructor(private statsService: StatsService) {}

  stats: StatsModel | null = null;

  async ngOnInit() {
    this.stats = await this.statsService.getStatistics();
  }
}
