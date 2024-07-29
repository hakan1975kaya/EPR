import { AuditEnum } from "src/app/enums/audit-enum.enum"

export class WebLogUpdateRequestModel {
  id!: number
  detail!: string
  date!: Date
  audit!: AuditEnum
}
