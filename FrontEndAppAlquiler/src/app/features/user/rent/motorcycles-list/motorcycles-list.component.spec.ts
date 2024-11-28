import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MotorcyclesListComponent } from './motorcycles-list.component';

describe('MotorcyclesListComponent', () => {
  let component: MotorcyclesListComponent;
  let fixture: ComponentFixture<MotorcyclesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MotorcyclesListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MotorcyclesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
