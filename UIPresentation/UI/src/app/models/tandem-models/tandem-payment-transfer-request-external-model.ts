import { MoneyTypeEnum } from "src/app/enums/money-type-enum.enum"
import { PaymentExternal } from "./payment-external"

export class TandemPaymentTransferRequestExternalModel {
    requestNumber!:string
    corporateCode!:number
    registrationNumber!:number
    moneyType!:MoneyTypeEnum
    paymentExternals!:PaymentExternal[]
}
