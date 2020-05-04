export interface IValidator<T> {
  IsValid(model: T): boolean;
}
