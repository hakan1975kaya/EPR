
import { OperationClaim } from "../operation-claim-models/operation-claim";
import { Menu } from "../menu-models/menu";
import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";

export class MenuOperationClaimSaveRequestModel {
    menu!:Menu
    operationClaims!:OperationClaim[]
    saveType!: SaveTypeEnum
}
