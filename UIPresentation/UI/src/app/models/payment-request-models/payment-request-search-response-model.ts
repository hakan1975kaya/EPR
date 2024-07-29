import { ComissionTypeEnum } from "src/app/enums/comission-type-enum.enum"
import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"
import { StatusEnum } from "src/app/enums/status-enum.enum"

export class PaymentRequestSearchResponseModel {
    id!: number
    requestNumber!: string
    corporateCode!: number
    corporateName!: string
    registrationNumber!: number
    moneyType!: MoneyTypeEnum
    quantity!:number
    amount!:number
    status!:StatusEnum
    optime!:Date
    isActive!: boolean
}
