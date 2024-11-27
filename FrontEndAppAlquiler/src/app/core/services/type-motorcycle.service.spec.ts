import { TestBed } from '@angular/core/testing';

import { TypeMotorcycleService } from './type-motorcycle.service';

describe('TypeMotorcycleService', () => {
  let service: TypeMotorcycleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TypeMotorcycleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
