import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>import('./feature/person/person.module').then(m => m.PersonModule),
  }
];
