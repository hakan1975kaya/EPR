import { AuditEnum } from "src/app/enums/audit-enum.enum"

export class WebLogGetListResponseModel {
    id!: number
    detail!: string
    date!: Date
    audit!: AuditEnum
}
