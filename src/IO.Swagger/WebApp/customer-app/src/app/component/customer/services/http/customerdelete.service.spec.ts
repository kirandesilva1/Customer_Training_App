import { TestBed } from '@angular/core/testing';

import { CustomerdeleteService } from './customerdelete.service';

describe('CustomerdeleteService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CustomerdeleteService = TestBed.get(CustomerdeleteService);
    expect(service).toBeTruthy();
  });
});
