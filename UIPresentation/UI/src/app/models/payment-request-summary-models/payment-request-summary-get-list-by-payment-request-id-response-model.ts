import { Time } from "@angular/common"
import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"
import { StatusEnum } from "src/app/enums/status-enum.enum"

export class PaymentRequestSummaryGetListByPaymentRequestIdResponseModel {
    id!:number
    paymentRequestId!:number
    status!:StatusEnum
    uploadDate!:Date
    quantity!:number
    amount!:number
    userId!:number
    systemEnteredDate!:Date
    systemEnteredTime!:Time
    systemEnteredRegistrationNumber!:number
    isActive!:boolean
}

