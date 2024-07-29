import { Time } from "@angular/common"
import { Timestamp } from "rxjs"
import { StatusEnum } from "src/app/enums/status-enum.enum"

export class PaymentRequestSummary {
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


