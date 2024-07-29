import { DataResult } from "../result-models/data-result"
import { LogParameter } from "./log-parameter"

export class Message {
    methodName!:string
    logParameters!:LogParameter[]
    date!:Date
    registrationNumber!:string
    remoteIpAddress!:string 
    response!:DataResult<any>
}
