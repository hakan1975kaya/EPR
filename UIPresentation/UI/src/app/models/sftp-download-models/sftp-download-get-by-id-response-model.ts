import { AuditEnum } from "src/app/enums/audit-enum.enum"
import { StatusEnum } from "src/app/enums/status-enum.enum"
export class SftpDownloadGetByIdResponseModel {
    id!: number
    corporateId!: number
    userId!: number
    sftpFileName!: string
    status!: StatusEnum
    optime!:Date
    isActive!:boolean
}
