import { TestBed } from '@angular/core/testing';

import { MotorcycleService } from './motorcycle.service';

describe('MotorcycleService', () => {
  let service: MotorcycleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MotorcycleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
