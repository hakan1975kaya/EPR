import { Injectable } from '@angular/core';

declare let alertify: any

@Injectable()
export class AlertifyService {

  constructor() { }

  success(message: string) {
    return alertify.success(message)
  }

  warnning(message: string) {
    return alertify.warnning(message)
  }

  error(message: string) {
    return alertify.error(message)
  }
}
