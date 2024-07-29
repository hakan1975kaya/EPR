import { AuditEnum } from "src/app/enums/audit-enum.enum"

export class ApiLogSearchRequestModel {
    filter!:string
    audit!:AuditEnum
}
