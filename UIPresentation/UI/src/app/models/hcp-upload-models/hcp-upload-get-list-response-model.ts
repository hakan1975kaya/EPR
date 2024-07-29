import { ComissionTypeEnum } from "src/app/enums/comission-type-enum.enum"
import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"

export class HcpUploadGetListResponseModel {
    id!:number
    paymentRequestId!:number
    hcpId!:string
    extension!:string
    explanation!:string
    optime!:Date
    isActive!:boolean
}
