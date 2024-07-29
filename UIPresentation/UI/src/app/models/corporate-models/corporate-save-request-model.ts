import { SaveTypeEnum } from "src/app/enums/save-type-enum.enum";
import { Corporate } from "./corporate";

export class CorporateSaveRequestModel {
    corporate!: Corporate
    saveType!: SaveTypeEnum
}
