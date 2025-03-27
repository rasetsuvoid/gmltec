import {FilterCondition} from './filter-condition';

export interface PaginationFilterRequest {
  pageNumber: number;
  pageSize: number;
  filters: FilterCondition[];
}
