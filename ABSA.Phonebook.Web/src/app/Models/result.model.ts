export interface ResultModel<T> {
  data: T;
  success: boolean;
  errorMessage: string;
}
