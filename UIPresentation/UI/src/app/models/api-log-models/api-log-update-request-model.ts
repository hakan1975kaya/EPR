import { AuditEnum } from "src/app/enums/audit-enum.enum"

export class ApiLogUpdateRequestModel {
    id!: number
    detail!: string
    date!: Date
    audit!:string
}
