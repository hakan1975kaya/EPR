
import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";
import { MailAddress } from "./mail-address";

export class MailAddressSaveRequestModel {
    mailAddress!:MailAddress
    saveType!: SaveTypeEnum
}
