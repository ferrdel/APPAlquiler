import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRentsComponent } from './user-rents.component';

describe('UserRentsComponent', () => {
  let component: UserRentsComponent;
  let fixture: ComponentFixture<UserRentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserRentsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserRentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
