import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'unknownBreed'
})
export class UnknownBreedPipe implements PipeTransform {

  transform(value: any | undefined | null): string {
    if (value === undefined || value === null ) {
      value = 'Breed Unknown'
      return value;
    }
    return value;
  }

}
