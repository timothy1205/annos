import { SubscriptonService } from '../../services/subscripton.service';
import { Component, OnInit } from '@angular/core';
import { SubscriptonComponent } from '../../components/subscripton/subscripton.component';
import { SubscriptionModel } from '../../models/subscription.model';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [SubscriptonComponent, NgFor],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
})
export class HomePageComponent implements OnInit {
  constructor(private subscriptionService: SubscriptonService) {}

  subscriptions: SubscriptionModel[] = [];

  async ngOnInit() {
    this.subscriptions = await this.subscriptionService.getSubscriptions();
  }
}
