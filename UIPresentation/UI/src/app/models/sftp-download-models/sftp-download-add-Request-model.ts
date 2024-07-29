import { StatusEnum } from "src/app/enums/status-enum.enum"
export class SftpDownloadAddRequestModel {
    id!: number
    corporateId!: number
    userId!: number
    sftpFileName!: string
    status!: StatusEnum
    optime!:Date
    isActive!:boolean
}
