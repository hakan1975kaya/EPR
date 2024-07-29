
import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";
import { UserModel } from "./user-model";

export class UserSaveRequestModel {
    user!: UserModel
    isPasswordSeted!:boolean
    saveType!:SaveTypeEnum
}
