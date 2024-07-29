import { ComissionTypeEnum } from "src/app/enums/comission-type-enum.enum"
import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"
import { StatusEnum } from "src/app/enums/status-enum.enum"

export class HcpUploadSearchResponseModel {
    id!: number
    corporateCode!: number
    corporateName!: string
    moneyType!: MoneyTypeEnum
    requestNumber!:string
    registrationNumber!: number
    quantity!: number
    amount!: number
    status!: StatusEnum
    optime!:Date
    hcpId!: string
    extension!:string
    explanation!:string
}



