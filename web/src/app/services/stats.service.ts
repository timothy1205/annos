import { Injectable } from '@angular/core';
import { StatsModel } from '../models/stats.model';

@Injectable({
  providedIn: 'root',
})
export class StatsService {
  private stats: StatsModel | null = null;

  async getStatistics(): Promise<StatsModel> {
    if (this.stats != null) return this.stats;

    const req = await fetch('http://localhost:8080/Stats/Get');
    const data = await req.json();

    data.active_subscriptions = (
      data.active_subscriptions as number
    ).toString();

    data.cost_per_month = this.formatCurrency(data.cost_per_month as number);
    data.cost_per_year = this.formatCurrency(data.cost_per_year as number);
    data.average_monthly_cost = this.formatCurrency(
      data.average_monthly_cost as number
    );
    data.top_cost = this.formatCurrency(data.top_cost as number);
    data.amount_due = this.formatCurrency(data.amount_due as number);

    return data;
  }

  public formatCurrency(value: number) {
    return '$' + Intl.NumberFormat().format(value);
  }
}
