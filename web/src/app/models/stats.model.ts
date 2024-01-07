export interface StatsModel {
  active_subscriptions: string;
  cost_per_month: string;
  cost_per_year: string;
  average_monthly_cost: string;
  top_cost: string;
  amount_due: string;
  category_splits: Array<{ name: string; cost: number }>;
}
