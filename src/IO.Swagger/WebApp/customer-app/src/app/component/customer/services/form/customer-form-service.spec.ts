import { TestBed } from '@angular/core/testing';

import { CustomerFormServiceService } from './customer-form-service.service';

describe('CustomerFormServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CustomerFormServiceService = TestBed.get(CustomerFormServiceService);
    expect(service).toBeTruthy();
  });
});
