import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatTime'
})
export class FormatTimePipe implements PipeTransform {

  transform(value: Date, ...args: unknown[]): string {
    let dt = new Date(value);
    let hour = dt.getHours();
    let minutes = (dt.getMinutes()).toString().padStart(2, '0');
    let part = hour >= 12 ? 'pm' : 'am';
         if(hour == 0) {
          hour = 12;
         }   
    hour = hour > 12 ? hour - 12 : hour;
    let newHour = hour.toString();
    newHour = (newHour+'').length == 1 ? `0${newHour}` : newHour;

    return `${newHour}:${minutes} ${part}`;
  }

}
