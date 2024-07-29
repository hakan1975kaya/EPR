import { Pipe, PipeTransform } from '@angular/core';
import { OrderTypeEnum } from 'src/app/enums/order-type-enum.enum';

@Pipe({
  name: 'order'
})
export class OrderPipe implements PipeTransform {


  transform(array: any[], columnName: string, orderType: OrderTypeEnum): any[] {
    if (orderType == OrderTypeEnum.Asc) {
      return array.sort((a, b) => (a[columnName] < b[columnName]) ? -1 : 1)
    }
    else if (orderType == OrderTypeEnum.Desc) {
      return array.sort((a, b) => (a[columnName] > b[columnName]) ? -1 : 1)
    }
    else {
      return array
    }
  }

}
