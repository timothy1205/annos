import { Component } from '@angular/core';
import { SubscriptonComponent } from '../../components/subscripton/subscripton.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [SubscriptonComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
})
export class HomePageComponent {
  subscriptionTest = {
    id: 1,
    name: 'Test Subscription',
    price: 5,
    frequency: 1,
    cycle: 1,
    subscriptionCategoryId: 1,
    nextPaymentDate: new Date(),
  };
}
