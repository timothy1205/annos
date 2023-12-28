import { Component, Input, OnInit } from '@angular/core';
import { SubscriptionModel } from '../../models/SubscriptionModel';

@Component({
  selector: 'app-subscripton',
  standalone: true,
  imports: [],
  templateUrl: './subscripton.component.html',
  styleUrl: './subscripton.component.css',
  host: { class: 'container' },
})
export class SubscriptonComponent implements OnInit {
  @Input({ required: true }) subscription!: SubscriptionModel;

  formattedDate: string = '';
  formattedPrice: string = '';

  ngOnInit() {
    const year = this.subscription.nextPaymentDate.getFullYear();
    const month = this.subscription.nextPaymentDate.getMonth() + 1;
    const day = this.subscription.nextPaymentDate.getDate();

    this.formattedDate = `${year}-${month}-${day}`;
    this.formattedPrice = '$' + this.subscription.price.toFixed(2);
  }
}
