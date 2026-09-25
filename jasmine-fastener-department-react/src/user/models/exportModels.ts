import type {StateBase} from "../../shared/models/models.ts";
import type {Template} from "./templateModels.ts";

export interface ExportPageState extends StateBase {
    templates: Template[];
}