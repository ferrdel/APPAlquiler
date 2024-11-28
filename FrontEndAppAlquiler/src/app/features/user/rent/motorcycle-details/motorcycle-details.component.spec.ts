import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MotorcycleDetailsComponent } from './motorcycle-details.component';

describe('MotorcycleDetailsComponent', () => {
  let component: MotorcycleDetailsComponent;
  let fixture: ComponentFixture<MotorcycleDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MotorcycleDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MotorcycleDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
