export class ApiResponse {
    success: boolean;
    data: any;
    businessErrors: ApiBusinessErrorCodes[];
}

export class ApiBusinessErrorCodes {
    errorCode: number;
    errorMessage: string;
}
