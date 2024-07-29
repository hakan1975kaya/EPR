import { ComissionTypeEnum } from "src/app/enums/comission-type-enum.enum"
import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"

export class CorporateGetByIdResponseModel {
    id!: number
    corporateCode!: number
    corporateName!: string
    corporateAccountNo!: number
    moneyType!: MoneyTypeEnum
    prefix!: string
    comissionType!: ComissionTypeEnum
    comissionMoneyType!: MoneyTypeEnum
    comission!: number
    comissionAccountNo!: number
    optime!:Date
    isActive!: boolean
}
