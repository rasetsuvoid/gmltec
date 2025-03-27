import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PersonRoutingModule} from './person-routing.module';
import { ListPersonComponent } from './list-person/list-person.component';
import { AddUserModalComponent } from './add-user-modal/add-user-modal.component';
import {ReactiveFormsModule} from '@angular/forms';
import { FilterComponent } from './filter/filter.component';



@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    PersonRoutingModule
  ]
})
export class PersonModule { }
