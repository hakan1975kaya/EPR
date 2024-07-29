
import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";
import { UserRole } from "./user-role";

export class UserRoleSaveRequestModel {
    userRole!:UserRole
    saveType!: SaveTypeEnum
}
