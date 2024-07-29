import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticationGuard } from './guards/authentication-guard';
import { AuthorizationGuard } from './guards/authorization-guard';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoginComponent } from './pages/login/login.component';
import { AppComponent } from './app.component';
import { UserComponent } from './pages/user/user.component';
import { OperationClaimComponent } from './pages/operation-claim/operation-claim.component';
import { MenuComponent } from './pages/menu/menu.component';
import { MenuOperationClaimComponent } from './pages/menu-operation-claim/menu-operation-claim.component';
import { ApiLogComponent } from './pages/api-log/api-log.component';
import { WebLogComponent } from './pages/web-log/web-log.component';
import { CorporateComponent } from './pages/corporate/corporate.component';
import { PaymentRequestComponent } from './pages/payment-request/payment-request.component';
import { PaymentRequestSummaryComponent } from './pages/payment-request-summary/payment-request-summary.component';
import { PasswordChangeComponent } from './pages/password-change/password-change.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { MailAddressComponent } from './pages/mail-address/mail-address.component';
import { CorporateMailAddressComponent } from './pages/corporate-mail-address/corporate-mail-address.component';
import { RoleComponent } from './pages/role/role.component';
import { RoleOperationClaimComponent } from './pages/role-operation-claim/role-operation-claim.component';
import { UserRoleComponent } from './pages/user-role/user-role.component';
import { HcpUploadComponent } from './pages/hcp-upload/hcp-upload.component';

const routes: Routes = [
  { path: 'app', component: AppComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'password-change', component: PasswordChangeComponent,canActivate:[AuthenticationGuard] },
  { path: 'user-profile', component: UserProfileComponent,canActivate:[AuthenticationGuard] },
  { path: 'dashboard', component: DashboardComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'user', component: UserComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'menu', component: MenuComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'operation-claim', component: OperationClaimComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'role', component: RoleComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'menu-operation-claim', component: MenuOperationClaimComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'role-operation-claim', component: RoleOperationClaimComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'user-role', component: UserRoleComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'api-log', component: ApiLogComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'web-log', component: WebLogComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'corporate', component: CorporateComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'mail-address', component: MailAddressComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'corporate-mail-address', component: CorporateMailAddressComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'payment-request', component: PaymentRequestComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'payment-request-summary', component: PaymentRequestSummaryComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: 'hcp-upload', component: HcpUploadComponent,canActivate:[AuthenticationGuard,AuthorizationGuard] },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
