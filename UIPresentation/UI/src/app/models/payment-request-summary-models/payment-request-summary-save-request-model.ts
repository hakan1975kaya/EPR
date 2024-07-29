import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum"
import { PaymentRequestSummary } from "./payment-request-summary"
import { HcpFileRequestModel } from "./hcp-file-request-model"

export class PaymentRequestSummarySaveRequestModel {
    paymentRequestSummaries!: PaymentRequestSummary[]
    saveType!: SaveTypeEnum
    requestNumber!: string
    hcpFileRequests!: HcpFileRequestModel[]
}
