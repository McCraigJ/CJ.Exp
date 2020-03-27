export class ApiResponse {
    success: boolean;
    data: any;
    businessErrors: ApiBusinessErrorCodes[];
}

export class ApiBusinessErrorCodes {
    errorCode: number;
    errorMessage: string;
}

export class GridResponse {
    currentPageNumber: number;
    totalRecordCount: number;    
    recordsPerPage: number;
    totalPages: number;    
    gridPageTotal?: number;
    dataSetTotal?: number;
}