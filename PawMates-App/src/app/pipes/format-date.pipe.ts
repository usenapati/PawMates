import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatDate'
})
export class FormatDatePipe implements PipeTransform {

  transform(value: Date, ...args: unknown[]): string {
    let dt = new Date(value);
    let month = (dt.getMonth() + 1).toString().padStart(2, "0");
    let day = dt.getDate();
    let year = dt.getFullYear();

    return `${month}/${day}/${year}`;
  }

}
