import { Time } from "@angular/common"
import { StatusEnum } from "src/app/enums/status-enum.enum"

export class PaymentRequestSummaryGetByIdResponseModel {
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
  optime!:Date
  isActive!:boolean
}
