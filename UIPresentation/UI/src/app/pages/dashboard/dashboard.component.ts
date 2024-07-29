import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { CorporateService } from 'src/app/services/corporate/corporate.service';
import { PaymentRequestSummaryService } from 'src/app/services/payment-request-summary/payment-request-summary.service';

import { TranslatePipe } from 'src/app/pipes/translate/translate.pipe';

import { MonthEnum } from 'src/app/enums/month-enum.enum';
import { StatusEnum } from 'src/app/enums/status-enum.enum';

import { CorporateGetListResponseModel } from 'src/app/models/corporate-models/corporate-get-list-response-model';
import { Year } from 'src/app/models/dashboard-models/year';
import { PaymentRequestSummaryAmountByCorporateIdYearRequestModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-amount-by-corporate-id-year-request-model';
import { PaymentRequestSummaryAmountByCorporateIdYearResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-amount-by-corporate-id-year-response-model';
import { PaymentRequestSummaryTotalOutgoingPaymentResponseModel } from 'src/app/models/payment-request-summary-models/payment-request-summary-total-outgoing-payment-response-model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [CorporateService, PaymentRequestSummaryService, TranslatePipe]
})
export class DashboardComponent implements OnInit {

  constructor(
    private corporateService: CorporateService,
    private paymentRequestSummaryService: PaymentRequestSummaryService,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private translatePipe: TranslatePipe,
  ) { }

  corporateCodeDefault = -1
  corporateGetListResponseModels!: CorporateGetListResponseModel[]

  yearDefault = -1
  years: Year[] = []

  dashboardForm!: FormGroup;

  paymentRequestSummaryAmountByCorporateIdYearRequestModel!: PaymentRequestSummaryAmountByCorporateIdYearRequestModel;
  paymentRequestSummaryAmountByCorporateIdYearResponseModels: PaymentRequestSummaryAmountByCorporateIdYearResponseModel[] = [];

  paymentRequestSummaryTotalOutgoingPaymentResponseModels:PaymentRequestSummaryTotalOutgoingPaymentResponseModel[]=[]

  ngOnInit() {
    this.createDashboardForm();
    this.getCorporates();
    this.getYears()
    this.getTotalOutgoingPayment();
  }

  createDashboardForm() {
    this.dashboardForm = this.formBuilder.group({
      corporateId: ['', [Validators.required, Validators.min(0), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9]*$")]],
      year: ['', [Validators.required, Validators.min(2000), Validators.max(Number.MAX_VALUE), Validators.pattern("^[0-9]*$")]],

    });
  }

  getCorporates() {
    this.corporateService.getList().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            this.corporateGetListResponseModels = dataResult.data
          }
        } else {
          this.alertifyService.error(dataResult.message);
        }
      }
    })
  }

  getYears() {
    for (let i = 0; i <= 10; i++) {
      let year: Year = new Year()
      year.name = (new Date().getFullYear() - 10 + i).toString()
      year.value = (new Date().getFullYear() - 10 + i);
      this.years.push(year)
    }
  }

  search() {
    this.paymentRequestSummaryAmountByCorporateIdYearRequestModel = Object.assign({}, this.dashboardForm.value)
    if (this.paymentRequestSummaryAmountByCorporateIdYearRequestModel) {
      this.paymentRequestSummaryService.amountByCorporateIdYear(this.paymentRequestSummaryAmountByCorporateIdYearRequestModel).subscribe(dataResult => {
        if (dataResult) {
          if (dataResult.success) {
            if (dataResult.data) {

              dataResult.data.forEach(summaryData => {
                summaryData.name = this.translatePipe.transform(MonthEnum[Number(summaryData.name)])
              })

              this.paymentRequestSummaryAmountByCorporateIdYearResponseModels = dataResult.data
            }
          }
        }
      })
    }
  }

  clear() {
    this.dashboardForm.controls["corporateId"].setValue(-1)
    this.dashboardForm.controls["year"].setValue(-1)
  }

  getTotalOutgoingPayment() {
    this.paymentRequestSummaryService.totalOutgoingPayment().subscribe(dataResult => {
      if (dataResult) {
        if (dataResult.success) {
          if (dataResult.data) {
            dataResult.data.forEach(summaryData => {
              summaryData.name = this.translatePipe.transform(StatusEnum[Number(summaryData.name)])
            })

            this.paymentRequestSummaryTotalOutgoingPaymentResponseModels = dataResult.data
          }
        }
      }
    })
  }
}

