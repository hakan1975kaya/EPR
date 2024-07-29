import { Pipe, PipeTransform } from '@angular/core';
import { OperationClaimGetListByUserIdResponseModel } from 'src/app/models/operation-claim-models/operation-claim-get-list-by-user-id-response-model';
import { AuthService } from 'src/app/services/auth/auth.service';

@Pipe({
  name: 'operationclaim'
})
export class OperationClaimPipe implements PipeTransform {

  constructor(private authService: AuthService) { }

  transform(operationClaimName: string): string {
    let isOperationClaim = false

    let operationClaimGetListByUserIdResponseModels: OperationClaimGetListByUserIdResponseModel[]

    let operationClaims = this.authService.getOperationClaims()?.toString()

    operationClaimGetListByUserIdResponseModels = JSON.parse(operationClaims == undefined ? "{}" : operationClaims)

    if (operationClaimGetListByUserIdResponseModels != null) {
      if (operationClaimGetListByUserIdResponseModels.length > 0) {
        operationClaimGetListByUserIdResponseModels.forEach(operationClaim => {
          if (operationClaim.name == operationClaimName) {
            isOperationClaim = true
          }
        })
      }
    }

    if (isOperationClaim) {
      return "block"
    }
    return "none"

  }

}
