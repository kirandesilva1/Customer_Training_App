import { TestBed } from '@angular/core/testing';

import { GetCustomerService } from './get-customer.service';

describe('GetCustomerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GetCustomerService = TestBed.get(GetCustomerService);
    expect(service).toBeTruthy();
  });
});
