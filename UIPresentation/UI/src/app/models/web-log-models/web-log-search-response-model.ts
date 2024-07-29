import { AuditEnum } from "src/app/enums/audit-enum.enum"

export class WebLogSearchResponseModel {
    id!: number
    detail!: string
    date!: Date
    audit!: AuditEnum
}

