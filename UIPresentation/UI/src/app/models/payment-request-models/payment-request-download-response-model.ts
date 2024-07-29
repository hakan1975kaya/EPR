import { PaymentRequestDetail } from "../payment-request-datail-models/payment-request-detail"
import { PaymentRequest } from "src/app/models/payment-request-models/payment-request"
export class PaymentRequestDownloadResponseModel {
    paymentRequest!:PaymentRequest
    paymentRequestDetails!:PaymentRequestDetail[]
}
