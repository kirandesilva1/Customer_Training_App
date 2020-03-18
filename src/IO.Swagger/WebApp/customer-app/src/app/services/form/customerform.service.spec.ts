import { TestBed } from '@angular/core/testing';

import { CustomerformService } from './customerform.service';

describe('CustomerformService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CustomerformService = TestBed.get(CustomerformService);
    expect(service).toBeTruthy();
  });
});
