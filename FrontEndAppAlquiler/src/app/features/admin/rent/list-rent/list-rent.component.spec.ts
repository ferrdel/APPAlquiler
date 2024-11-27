import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListRentComponent } from './list-rent.component';

describe('ListRentComponent', () => {
  let component: ListRentComponent;
  let fixture: ComponentFixture<ListRentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListRentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListRentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
