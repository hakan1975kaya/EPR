import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { UserGetByRegistrationNumberResponseModel } from 'src/app/models/user-models/user-get-by-user-name-response-model'
@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  constructor(
    private authService: AuthService
  ) { }
  userGetByRegistrationNumberResponseModel: UserGetByRegistrationNumberResponseModel = new UserGetByRegistrationNumberResponseModel()
  ngOnInit(): void {
    this.userGetByRegistrationNumberResponseModel = this.authService.getCurrentUser();
  }

  isLogin() {
    return this.authService.isLogIn()
  }
}
