import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";
import { Menu } from "./menu";

export class MenuSaveRequestModel {
    menu!: Menu
    saveType!: SaveTypeEnum
}
