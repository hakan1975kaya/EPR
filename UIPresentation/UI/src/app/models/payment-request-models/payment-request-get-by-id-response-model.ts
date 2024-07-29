import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"
import { StatusEnum } from "src/app/enums/status-enum.enum"

export class PaymentRequestGetByIdResponseModel {
    id!: number
    requestNumber!: string
    corporateId!: number
    userId!: number
    fileId!: number
    moneyType!: MoneyTypeEnum
    status!:StatusEnum
    optime!:Date
    isActive!: boolean
}
