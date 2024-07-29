import { Time } from "@angular/common"
import { ComissionTypeEnum } from "src/app/enums/comission-type-enum.enum"
import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"
import { StatusEnum } from "src/app/enums/status-enum.enum"

export class PaymentRequestSummarySearchResponseModel {
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
