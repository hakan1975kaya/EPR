import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"

export class PaymentRequestDetailGetListResponseModel {
    id!: number
    paymentRequestId!: number
    referenceNumber!: string
    accountNumber!: number
    customerNumber!: number
    phoneNumber!: string
    firstName!: string
    lastName!: string
    paymentAmount!: number
    cardDepositDate!: Date
    explanation!:string
    optime!:Date
    isActive!:boolean
}
