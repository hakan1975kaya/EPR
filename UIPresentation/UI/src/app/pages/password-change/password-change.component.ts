import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Router } from '@angular/router'

import { UserService } from 'src/app/services/User/User.service'
import { AuthService } from 'src/app/services/auth/auth.service'
import { AlertifyService } from 'src/app/services/alertify/alertify.service'

import { UserGetByRegistrationNumberResponseModel } from 'src/app/models/user-models/user-get-by-user-name-response-model'
import { PasswordChangeRequestModel } from 'src/app/models/user-models/password-change-request-model'


@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css'],
  providers: [UserService]
})
export class PasswordChangeComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private router:Router
  ) { }

  passwordChangeForm!: FormGroup

  passwordChangeRequestModel!: PasswordChangeRequestModel

  userGetByRegistrationNumberResponseModel: UserGetByRegistrationNumberResponseModel = new UserGetByRegistrationNumberResponseModel()

  ngOnInit(): void {
    this.createPasswordChangeForm()
    this.userGetByRegistrationNumberResponseModel = this.authService.getCurrentUser();
    if (this.userGetByRegistrationNumberResponseModel) {
      if (this.userGetByRegistrationNumberResponseModel.registrationNumber) {
        this.passwordChangeForm.controls["registrationNumber"].setValue(this.userGetByRegistrationNumberResponseModel.registrationNumber)
      }
    }
  }

  createPasswordChangeForm() {
    this.passwordChangeForm = this.formBuilder.group({
      registrationNumber: ["", Validators.required],
      password: ["", Validators.required]
    })
  }

  changePassword() {
    if (this.passwordChangeForm.valid) {
      this.passwordChangeRequestModel = Object.assign({}, this.passwordChangeForm.value)
      if (this.passwordChangeRequestModel) {
        this.userService.passwordChange(this.passwordChangeRequestModel).subscribe(dataResult => {
          if (dataResult) {
            if (dataResult.success) {
              this.alertifyService.success(dataResult.message)
              this.router.navigateByUrl('dashboard')
            }
            else {
              this.alertifyService.error(dataResult.message)
            }
          }
        })
      }
    }
  }
}