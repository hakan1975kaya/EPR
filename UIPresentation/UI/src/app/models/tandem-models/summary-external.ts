import { Time } from "@angular/common"
import { StatusEnum } from "src/app/enums/status-enum.enum"
export class SummaryExternal {
    requestNumber!:string
    corporateCode!:number
    status!:StatusEnum
    uploadDate!:Date
    quantity!:number
    amount!:number
    registrationNumber!:number
    systemEnteredDate!:Date
    systemEnteredTime!:Time
    systemEnteredRegistrationNumber!:number
    isChecked!:boolean
}
