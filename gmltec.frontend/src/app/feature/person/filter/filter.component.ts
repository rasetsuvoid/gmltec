import {Component, Inject} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import {MatButton} from '@angular/material/button';
import {FilterCondition} from '../../../core/models/filter-condition';
import {MatGridList, MatGridTile} from '@angular/material/grid-list';
import {MatFormField, MatInput} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-filter',
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDialogContent,
    ReactiveFormsModule,
    MatDialogActions,
    MatButton,
    MatGridList,
    MatGridTile,
    MatFormFieldModule,
    MatInput
  ],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterComponent {
  filterForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<FilterComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.filterForm = this.fb.group({
      name: [data?.name || ''],
      lastName: [data?.lastName || ''],
      documentNumber: [data?.documentNumber || '']
    });
  }

  applyFilter(): void {
    const { name, lastName, documentNumber } = this.filterForm.value;
    const filters: FilterCondition[] = [];

    if (name) {
      filters.push({ property: 'firstName', value: name, operator: 'contains' });
    }
    if (lastName) {
      filters.push({ property: 'lastName', value: lastName, operator: 'contains' });
    }
    if (documentNumber) {
      filters.push({ property: 'documentNumber', value: documentNumber, operator: 'contains' });
    }

    this.dialogRef.close(filters);
  }

  clearFilter(): void {
    this.filterForm.reset();
  }

  close(): void {
    this.dialogRef.close(null);
  }
}
