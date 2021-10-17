import { Message } from "./Message";

export interface Pagination {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

export class PaginatedResult {
    result: Message[];
    pagination: Pagination;
    constructor(){
      this.result = [];
      this.pagination = {currentPage:0, itemsPerPage:0,totalItems:0,totalPages:0};
    }
}
