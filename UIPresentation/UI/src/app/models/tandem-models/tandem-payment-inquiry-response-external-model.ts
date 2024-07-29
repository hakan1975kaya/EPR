import { SummaryExternal } from "./summary-external"

export class TandemPaymentInquiryResponseExternalModel {
    responseCode!:string
    responseMessage!:string
    summaryExternals!:SummaryExternal[]
}
