import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CountryGridModel } from 'src/app/models/country/country-grid-model';
import { Employee } from 'src/app/models/employee/employee';
import { CountryService } from 'src/app/services/country.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {

  employeeForm!: FormGroup;
  countries: CountryGridModel[] = [];
  isSubmitting: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private employeeService: EmployeeService,
    private toastrService: ToastrService,
    private countryService: CountryService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.createForm();
    this.getCountries();
  }

  private createForm(): void {
    this.employeeForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      middleName: [''],
      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      phone: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      countryId: [null, Validators.required]
    });
  }

  private getCountries(): void {
    this.countryService.getAllAsync().subscribe((result: CountryGridModel[]) => {
      this.countries = result;
    }, error => {
      this.toastrService.error('Failed to load countries.', 'Error');
    });
  }

  onSubmitEmployeeData(): void {
    if (this.employeeForm.invalid) {
      this.toastrService.error('Invalid Data Provided', 'Error');
      return;
    }

    this.isSubmitting = true;
    const employee: Employee = this.employeeForm.value;

    this.employeeService.createEmployee(employee).subscribe(
      (result) => {
        this.isSubmitting = false;
        this.toastrService.success('Employee created successfully', 'Success');
        this.router.navigate(['/employees']);
      },
      (error) => {
        this.isSubmitting = false;
        this.toastrService.error('An error occurred while creating the employee', 'Error');
      }
    );
  }

}
