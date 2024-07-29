import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { AppRoutingModule } from './app-routing.module';
import { NgxChartsModule }from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PipeService } from './services/pipe/pipe.service';
import { AuthService } from './services/auth/auth.service';
import { AlertifyService } from './services/alertify/alertify.service';
import { MenuService } from './services/menu/menu.service';
import { UserService } from './services/User/User.service';

import { TranslatePipe } from './pipes/translate/translate.pipe';
import { OperationClaimPipe } from './pipes/operation-claim/operation-claim.pipe';

import { AuthenticationGuard } from './guards/authentication-guard';
import { AuthorizationGuard } from './guards/authorization-guard';

import { AppComponent } from './app.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoginComponent } from './pages/login/login.component';
import { UserComponent } from './pages/user/user.component';
import { OperationClaimComponent } from './pages/operation-claim/operation-claim.component';
import { MenuComponent } from './pages/menu/menu.component';
import { MenuOperationClaimComponent } from './pages/menu-operation-claim/menu-operation-claim.component';
import { ApiLogComponent } from './pages/api-log/api-log.component';
import { WebLogComponent } from './pages/web-log/web-log.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CorporateComponent } from './pages/corporate/corporate.component';
import { PaymentRequestComponent } from './pages/payment-request/payment-request.component';
import { PaymentRequestSummaryComponent } from './pages/payment-request-summary/payment-request-summary.component';
import { OrderPipe } from './pipes/order/order.pipe';
import { PasswordChangeComponent } from './pages/password-change/password-change.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { MailAddressComponent } from './pages/mail-address/mail-address.component';
import { CorporateMailAddressComponent } from './pages/corporate-mail-address/corporate-mail-address.component';
import { RoleComponent } from './pages/role/role.component';
import { RoleOperationClaimComponent } from './pages/role-operation-claim/role-operation-claim.component';
import { UserRoleComponent } from './pages/user-role/user-role.component';
import { HcpUploadComponent } from './pages/hcp-upload/hcp-upload.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    UserComponent,
    OperationClaimPipe,
    TranslatePipe,
    OperationClaimComponent,
    MenuComponent,
    MenuOperationClaimComponent,
    ApiLogComponent,
    WebLogComponent,
    CorporateComponent,
    PaymentRequestComponent,
    PaymentRequestSummaryComponent,
    PasswordChangeComponent,
    UserProfileComponent,
    MailAddressComponent,
    CorporateMailAddressComponent,
    RoleComponent,
    RoleOperationClaimComponent,
    UserRoleComponent,
    HcpUploadComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    NgMultiSelectDropDownModule.forRoot(),
    NgbModule,
    BrowserAnimationsModule,
    NgxChartsModule
  ],
  providers: [
    PipeService,
    AuthService,
    AlertifyService,
    UserService,
    AuthorizationGuard,
    AuthenticationGuard,
    MenuService,
    OrderPipe
  ],
  bootstrap: [AppComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class AppModule { }
