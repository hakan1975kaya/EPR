import { Pipe, PipeTransform } from '@angular/core';
import TranslatesJson from '../../../assets/translate/translates.json';  
import { TranslateModel } from 'src/app/models/translate-models/translateModel';

@Pipe({
  name: 'translate'
})

export class TranslatePipe implements PipeTransform {

  translateModels!: TranslateModel[]
  constructor() {
    this.translateModels =Object.assign( [], TranslatesJson); 
  }

  transform(english: string): string {
    return  this.translateModels.filter(x => x.english == english).length > 0
      ?  this.translateModels.filter(x => x.english == english)[0].turkish
      : english
  }
}