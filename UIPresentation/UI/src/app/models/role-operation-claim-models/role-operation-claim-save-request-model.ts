
import { OperationClaim } from "../operation-claim-models/operation-claim";
import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";
import { Role } from "../role-models/role";

export class RoleOperationClaimSaveRequestModel {
    role!:Role
    operationClaims!:OperationClaim[]
    saveType!: SaveTypeEnum
}
