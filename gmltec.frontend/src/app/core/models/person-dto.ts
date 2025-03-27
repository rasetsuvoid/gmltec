export interface PersonDto {
  id: number;
  name: string;
  lastName: string;
  documentNumber: string;
  documentTypeId: number;
  documentType?: string;
  dateOfBirth: string;
  salary: number;
  maritalStatus: boolean;
}
