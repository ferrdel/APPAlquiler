import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListMotorcycleComponent } from './list-motorcycle.component';

describe('ListMotorcycleComponent', () => {
  let component: ListMotorcycleComponent;
  let fixture: ComponentFixture<ListMotorcycleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListMotorcycleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListMotorcycleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
