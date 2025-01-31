import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CountryGridModel } from 'src/app/models/country/country-grid-model';
import { Employee } from 'src/app/models/employee/employee';
import { CountryService } from 'src/app/services/country.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-update',
  templateUrl: './employee-update.component.html',
  styleUrls: ['./employee-update.component.css']
})
export class EmployeeUpdateComponent implements OnInit {

  employeeForm!: FormGroup;
  employeeId!: number;
  employee!: Employee;
  countries: CountryGridModel[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private toastrService: ToastrService,
    private spinnerService: NgxSpinnerService,
    private countryService: CountryService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.route.params.subscribe(params => {
      this.employeeId = +params["recordId"];
    });
  }

  ngOnInit(): void {

    this.employeeForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      middleName: [''],
      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      countryId: [null, Validators.required]
    });

    this.getCountries();
    this.getEmployeeDetails();
  }

  private getCountries(): void {
    this.spinnerService.show();
    this.countryService.getAllAsync().subscribe(
      (result: CountryGridModel[]) => {
        this.countries = result;
        this.spinnerService.hide();
      },
      (error: any) => {
        this.spinnerService.hide();
        this.toastrService.error('Countries could not be loaded!', 'Error');
      }
    );
  }

  private getEmployeeDetails(): void {
    this.spinnerService.show();
    this.employeeService.getEmployeeById(this.employeeId).subscribe(
      (result: Employee) => {
        this.employee = result;
        this.employeeForm.patchValue(this.employee);
        this.spinnerService.hide();
      },
      (error: any) => {
        this.spinnerService.hide();
        this.toastrService.error('Employee details could not be loaded!', 'Error');
      }
    );
  }

  updateEmployee(): void {
    if (this.employeeForm.invalid) {
      this.toastrService.error('Invalid Data Provided', 'Error');
      return;
    }

    const updatedEmployee = { ...this.employeeForm.value };
    updatedEmployee.id = this.employeeId;

    this.spinnerService.show();
    this.employeeService.updateEmployee(this.employeeId, updatedEmployee).subscribe(
      (result) => {
        this.spinnerService.hide();
        this.toastrService.success('Employee updated successfully.', 'Success');
        this.router.navigate(['/employee/list']);
      },
      (error: any) => {
        this.spinnerService.hide();
        this.toastrService.error('Employee could not be updated!', 'Error');
      }
    );
  }

}
