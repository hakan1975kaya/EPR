
import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";
import { OperationClaim } from "./operation-claim";

export class OperationClaimSaveRequestModel {
    operationClaim!: OperationClaim
    saveType!: SaveTypeEnum
}
