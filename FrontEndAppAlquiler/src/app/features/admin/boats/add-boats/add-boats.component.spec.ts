import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBoatsComponent } from './add-boats.component';

describe('AddBoatsComponent', () => {
  let component: AddBoatsComponent;
  let fixture: ComponentFixture<AddBoatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddBoatsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBoatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
