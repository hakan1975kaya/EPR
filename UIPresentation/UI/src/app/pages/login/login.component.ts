import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'

import { AuthService } from '../../services/auth/auth.service'

import { LoginRequestModel } from 'src/app/models/auth-models/login-request-model'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: []
})
export class LoginComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
  ) { }

  loginForm!: FormGroup

  loginRequestModel!: LoginRequestModel

  ngOnInit(): void {
    this.createLoginForm()
  }

  createLoginForm() {
    this.loginForm = this.formBuilder.group({
      registrationNumber: ["", Validators.required],
      password: ["", Validators.required]
    })
  }

  login() {
    if (this.loginForm.valid) {
      this.loginRequestModel = Object.assign({}, this.loginForm.value)
      if (this.loginRequestModel) {
        this.authService.login(this.loginRequestModel)
      }
    }
  }
}
