import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {PersonDto} from '../../../core/models/person-dto';
import {UserService} from '../../../core/services/user.service';
import {PaginationFilterRequest} from '../../../core/models/pagination-filter-request';
import {FilterCondition} from '../../../core/models/filter-condition';
import {CurrencyPipe, DatePipe, NgForOf, NgIf} from '@angular/common';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AddUserModalComponent} from '../add-user-modal/add-user-modal.component';
import {MatDialog} from '@angular/material/dialog';
import {FilterComponent} from '../filter/filter.component';
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef, MatHeaderRow,
  MatHeaderRowDef, MatRow, MatRowDef,
  MatTable, MatTableDataSource
} from '@angular/material/table';
import {MatButton, MatIconButton} from '@angular/material/button';
import {MatPaginator} from '@angular/material/paginator';
import {MatIcon} from '@angular/material/icon';
import {MatTooltip} from '@angular/material/tooltip';
import {MatCard, MatCardContent} from '@angular/material/card';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-list-person',
  imports: [
    CurrencyPipe,
    DatePipe,
    FormsModule,
    NgForOf,
    ReactiveFormsModule,
    MatButton,
    MatTable,
    MatColumnDef,
    MatHeaderCell,
    MatHeaderCellDef,
    MatCellDef,
    MatCell,
    MatHeaderRowDef,
    MatHeaderRow,
    MatRow,
    MatRowDef,
    MatPaginator,
    MatIcon,
    MatIconButton,
    MatTooltip,
    NgIf,
    MatCard,
    MatCardContent,
  ],
  templateUrl: './list-person.component.html',
  styleUrl: './list-person.component.css'
})
export class ListPersonComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['id', 'name', 'lastName', 'documentNumber', 'documentType', 'dateOfBirth', 'salary', 'maritalStatus', 'actions'];
  dataSource = new MatTableDataSource<PersonDto>([]);
  filters: FilterCondition[] = [];
  filterForm: FormGroup;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private usersService: UserService,
    private fb: FormBuilder,
    private dialog: MatDialog
  ) {
    this.filterForm = this.fb.group({
      name: [''],
      lastName: [''],
      documentNumber: ['']
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  loadUsers(): void {
    this.filters = this.filters.filter(f => f.property !== "active");

    this.filters.push({ property: "active", value: true });

    const filterRequest: PaginationFilterRequest = {
      pageNumber: this.paginator?.pageIndex + 1 || 1,
      pageSize: this.paginator?.pageSize || 5,
      filters: this.filters
    };

    this.usersService.getPersons(filterRequest).subscribe(response => {
      this.dataSource.data = response.data ?? [];
      this.paginator.length = response.totalRecords ?? 0;
    });
  }


  applyFilter(): void {
    this.loadUsers();
  }

  openAddUserDialog(): void {
    const dialogRef = this.dialog.open(AddUserModalComponent, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.usersService.createPerson(result).subscribe(() => {
          Swal.fire({
            title: 'Usuario Agregado',
            text: 'El usuario ha sido agregado correctamente.',
            icon: 'success',
            confirmButtonText: 'OK'
          });
          this.loadUsers();
        });
      }
    });
  }


  editUser(user: any): void {
    const dialogRef = this.dialog.open(AddUserModalComponent, {
      width: '600px',
      data: { user }
    });

    dialogRef.afterClosed().subscribe(updatedUser => {
      if (updatedUser) {
        this.usersService.updatePerson(user.id, updatedUser).subscribe({
          next: () => {
            this.loadUsers();
            Swal.fire({
              icon: 'success',
              title: 'Usuario actualizado',
              text: 'El usuario ha sido actualizado correctamente.',
              timer: 2000,
              showConfirmButton: false
            });
          },
          error: (err) => {
            Swal.fire({
              icon: 'error',
              title: 'Error',
              text: 'Hubo un problema al actualizar el usuario.',
              footer: err?.message || ''
            });
          }
        });
      }
    });
  }

  deleteUser(user: any): void {
    Swal.fire({
      title: '¿Estás seguro?',
      text: `¿Seguro que deseas eliminar a ${user.name} ${user.lastName}?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.usersService.deletePerson(user.id).subscribe(() => {
          Swal.fire('Eliminado', 'El usuario ha sido eliminado correctamente.', 'success');
          this.loadUsers();
        });
      }
    });
  }


  openFilterDialog(): void {
    const dialogRef = this.dialog.open(FilterComponent, {
      width: '600px',
      data: this.filters
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.filters = result;
        this.applyFilter();
      }
    });
  }

  clearFilters(): void {
    this.filters = [];
    this.loadUsers();
  }

}
