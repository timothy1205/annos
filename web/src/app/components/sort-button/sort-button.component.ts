import { KeyValuePipe, NgFor, NgIf } from '@angular/common';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { SubscriptionModel } from '../../models/subscription.model';

enum SortOption {
  NAME = 'sort-name',
  ID = 'sort-id',
  PRICE = 'sort-price',
  NEXT_PAYMENT = 'sort-next-payment',
  CATEGORY = 'sort-category-id',
}

@Component({
  selector: 'app-sort-button',
  standalone: true,
  imports: [NgFor, NgIf, KeyValuePipe],
  templateUrl: './sort-button.component.html',
  styleUrl: './sort-button.component.css',
})
export class SortButtonComponent implements OnChanges {
  @Input({ required: true }) subscriptions: SubscriptionModel[] = [];

  showSort = false;
  sortOptions = new Map([
    [SortOption.NAME, 'Name'],
    [SortOption.ID, 'Last Added'],
    [SortOption.PRICE, 'Price'],
    [SortOption.NEXT_PAYMENT, 'Next payment'],
    [SortOption.CATEGORY, 'Category'],
  ]);
  sortOptionEntries = this.sortOptions.keys();
  activeSortOption = SortOption.NEXT_PAYMENT;

  ngOnChanges() {
    this.callSortMethod(this.activeSortOption);
  }

  toggleSortPopup() {
    this.showSort = !this.showSort;
  }

  onSortClick(opt: SortOption = this.activeSortOption) {
    this.callSortMethod(opt);
    this.toggleSortPopup();
    this.activeSortOption = opt;
  }

  private callSortMethod(opt: SortOption) {
    switch (opt) {
      case SortOption.NAME:
        this.sortByName();
        break;
      case SortOption.ID:
        this.sortById();
        break;
      case SortOption.PRICE:
        this.sortByPrice();
        break;
      case SortOption.NEXT_PAYMENT:
        this.sortByNextPayment();
        break;
      case SortOption.CATEGORY:
        this.sortByCategory();
        break;
    }
  }

  private sortByName() {
    this.subscriptions.sort((a, b) => {
      if (a.name < b.name) return -1;
      if (a.name > b.name) return 1;
      return 0;
    });
  }

  private sortById() {
    this.subscriptions.sort((a, b) => a.id - b.id);
  }

  private sortByPrice() {
    this.subscriptions.sort((a, b) => a.price - b.price);
  }

  private sortByNextPayment() {
    this.subscriptions.sort(
      (a, b) => a.nextPaymentDate.getTime() - b.nextPaymentDate.getTime()
    );
  }

  private sortByCategory() {
    this.subscriptions.sort(
      (a, b) => a.subscriptionCategoryId - b.subscriptionCategoryId
    );
  }
}
