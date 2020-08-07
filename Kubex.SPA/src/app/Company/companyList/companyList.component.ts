import { Company } from './../../_models/company';
import { AlertService } from './../../_services/alert.service';
import { Component, OnInit, ModuleWithComponentFactories } from '@angular/core';
import { CompanyService } from 'src/app/_services/company.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-companylist',
  templateUrl: './companyList.component.html',
  styleUrls: ['./companyList.component.css']
})
export class CompanyListComponent implements OnInit {
  companies = Array<Company>();

  constructor(
    private companyService: CompanyService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.companyService.getCompanies()
    .pipe(first())
    .subscribe(companies => this.companies = companies);

    console.log("Companies: " + this.companies);
  }

}
