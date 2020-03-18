import { TestBed } from '@angular/core/testing';

import { CustomerRepositoryService } from './customer-repository.service';

describe('CustomerRepositoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CustomerRepositoryService = TestBed.get(CustomerRepositoryService);
    expect(service).toBeTruthy();
  });
});
