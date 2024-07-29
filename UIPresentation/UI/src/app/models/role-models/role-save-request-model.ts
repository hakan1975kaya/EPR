import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum"
import { Role } from "./role"

export class RoleSaveRequestModel {
    role!: Role
    saveType!: SaveTypeEnum
}
