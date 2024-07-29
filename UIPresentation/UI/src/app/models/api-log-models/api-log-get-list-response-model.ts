import { AuditEnum } from "src/app/enums/audit-enum.enum"

export class ApiLogGetListResponseModel {
    id!: number
    detail!: string
    date!: Date
    audit!:string
}
