import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"

export class CorporateExternal {
    corporateCode!:number
    corporateName!:string
    corporateAccountNo!:number
    moneyType!:MoneyTypeEnum
    prefix!:string
}