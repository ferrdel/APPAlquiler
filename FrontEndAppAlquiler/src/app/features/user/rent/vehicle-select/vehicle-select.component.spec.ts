import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleSelectComponent } from './vehicle-select.component';

describe('VehicleSelectComponent', () => {
  let component: VehicleSelectComponent;
  let fixture: ComponentFixture<VehicleSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehicleSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VehicleSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
