import { TestBed } from '@angular/core/testing';

import { SessionStorageManageService } from './session-storage-manage.service';

describe('SessionStorageManageService', () => {
  let service: SessionStorageManageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SessionStorageManageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
