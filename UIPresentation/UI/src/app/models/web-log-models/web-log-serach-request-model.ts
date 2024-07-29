import { AuditEnum } from "src/app/enums/audit-enum.enum"

export class WebLogSearchRequestModel {
    filter!:string
    audit!:AuditEnum
}
