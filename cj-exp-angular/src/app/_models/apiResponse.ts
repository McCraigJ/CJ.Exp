export class apiResponse {
    success:boolean;
    data: any;
    businessErrors: apiBusinessErrorCodes[]
}

export class apiBusinessErrorCodes {
    errorCode: Number;
    errorMessage: string;
}