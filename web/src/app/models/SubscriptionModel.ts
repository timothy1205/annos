export interface SubscriptionModel {
  id: number;
  name: string;
  price: number;
  frequency: number;
  cycle: number;
  subscriptionCategoryId: number;
  nextPaymentDate: Date;
}
