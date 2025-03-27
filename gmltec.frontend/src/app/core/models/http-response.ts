export interface HttpResponse<T> {
  httpStatusCode: number;
  message: string;
  data?: T;
  errors?: string[];
  totalRecords?: number;
  pageNumber?: number;
  pageSize?: number;
  totalPages?: number;
}
