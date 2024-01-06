import { Injectable } from '@angular/core';
import { SubscriptionModel } from '../models/SubscriptionModel';

@Injectable({
  providedIn: 'root',
})
export class SubscriptonService {
  async getSubscriptions(): Promise<SubscriptionModel[]> {
    const req = await fetch('http://localhost:8080/Subscriptions/GetAll');
    const data: any[] = await req.json();
    if (!data) {
      return [];
    }

    data.forEach((sub) => {
      sub.nextPaymentDate = new Date(sub.nextPaymentDate);
    });

    return data;
  }
}
