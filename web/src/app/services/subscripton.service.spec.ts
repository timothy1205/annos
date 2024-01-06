import { TestBed } from '@angular/core/testing';

import { SubscriptonService } from './subscripton.service';

describe('SubscriptonService', () => {
  let service: SubscriptonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubscriptonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
