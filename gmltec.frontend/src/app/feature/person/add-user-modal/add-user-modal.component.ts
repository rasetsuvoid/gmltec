import {Component, EventEmitter, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from '@angular/material/dialog';
import {MatFormField, MatInput, MatInputModule, MatLabel} from '@angular/material/input';
import {MatButton} from '@angular/material/button';
import {NgForOf} from '@angular/common';
import {MatOption, MatSelect} from '@angular/material/select';
import {DocumentTypeService} from '../../../core/services/document-type.service';
import {DocumentTypeDto} from '../../../core/models/document-type-dto';
import {
  MatDatepicker,
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle
} from '@angular/material/datepicker';
import {provideNativeDateAdapter} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-add-user-modal',
  imports: [
    MatFormField,
    ReactiveFormsModule,
    MatDialogContent,
    MatDialogTitle,
    MatInput,
    MatLabel,
    MatFormField,
    MatDialogActions,
    MatButton,
    MatSelect,
    NgForOf,
    MatOption,
    MatFormFieldModule, MatInputModule, MatDatepickerModule
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './add-user-modal.component.html',
  styleUrl: './add-user-modal.component.css'
})
export class AddUserModalComponent implements OnInit {
  addUserForm: FormGroup;
  documentTypes: DocumentTypeDto[] = [];

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddUserModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private documentTypeService: DocumentTypeService
  ) {
    this.addUserForm = this.fb.group({
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      documentNumber: ['', Validators.required],
      documentTypeId: [null, Validators.required],
      dateOfBirth: [null, Validators.required],
      salary: [0, Validators.required],
      maritalStatus: [true, Validators.required]
    });
  }

  ngOnInit(): void {
    this.getDocumentType();

    // Si hay datos, es edición: llenar el formulario
    if (this.data?.user) {
      this.addUserForm.patchValue(this.data.user);
    }
  }

  submit(): void {
    if (this.addUserForm.valid) {
      this.dialogRef.close(this.addUserForm.value);
    }
  }

  close(): void {
    this.dialogRef.close();
  }

  getDocumentType(): void {
    this.documentTypeService.getDocumentTypes().subscribe(
      response => {
        this.documentTypes = response.data ?? [];
      }
    );
  }
}
