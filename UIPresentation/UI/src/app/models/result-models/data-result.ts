import { Result } from "./result";

export class DataResult<T> extends Result {
  data!: T
}
