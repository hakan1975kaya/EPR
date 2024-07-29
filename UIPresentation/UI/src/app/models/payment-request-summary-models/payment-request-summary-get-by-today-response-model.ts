import { Time } from "@angular/common"
import { StatusEnum } from "src/app/enums/status-enum.enum"

export class PaymentRequestSummaryGetByTodayResponseModel {
    id!:number
    paymentRequestId!:number
    requestNumber!:string
    corporateCode!:number
    corporateName!: string
    status!:StatusEnum
    uploadDate!:Date
    quantity!:number
    amount!:number
    registrationNumber!:number
    systemEnteredDate!:Date
    systemEnteredTime!:Time
    systemEnteredRegistrationNumber!:number
    isActive!:boolean
}
