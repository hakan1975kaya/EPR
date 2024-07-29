
import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";
import { CorporateMailAddress } from "./corporate-mail-address";

export class CorporateMailAddressSaveRequestModel {
    corporateMailAddress!:CorporateMailAddress
    saveType!: SaveTypeEnum
}
