import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum"
import { PaymentRequest } from "src/app/models/payment-request-models/payment-request"
import { PaymentRequestDetail } from "../payment-request-datail-models/payment-request-detail"

export class PaymentRequestSaveRequestModel {
    paymentRequest!: PaymentRequest
    paymentRequestDetails!:PaymentRequestDetail[]
    saveType!: SaveTypeEnum
}
