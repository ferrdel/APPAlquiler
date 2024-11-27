import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateRentComponent } from './date-rent.component';

describe('DateRentComponent', () => {
  let component: DateRentComponent;
  let fixture: ComponentFixture<DateRentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DateRentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DateRentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
