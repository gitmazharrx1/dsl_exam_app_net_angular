import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Employee } from 'src/app/models/employee/employee';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: Employee[] = [];

  constructor(
    private employeeService: EmployeeService,
    private toastrService: ToastrService,
    private spinnerService: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getEmployees();
  }

  private getEmployees(): void {
    this.spinnerService.show();
    this.employeeService.getAllEmployees().subscribe(
      (result: Employee[]) => {
        this.employees = result;
        this.spinnerService.hide();
      },
      (error: any) => {
        this.spinnerService.hide();
        this.toastrService.error("Employees cannot be loaded! Please, try again.", "Error");
      }
    );
  }

  onClickDelete(employeeId: number): void {
    this.spinnerService.show();
    this.employeeService.deleteEmployee(employeeId).subscribe(
      (result) => {
        this.spinnerService.hide();
        this.getEmployees();
        this.toastrService.success("Employee deleted successfully.", "Success");
      },
      (error: any) => {
        this.spinnerService.hide();
        this.toastrService.error("Employee could not be deleted! Please, try again.", "Error");
      }
    );
  }
}
