import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environments";
import { Employee } from "../models/employee/employee";

@Injectable({
    providedIn: 'root'
})
export class EmployeeService {
    private apiUrl = `${environment.apiUrl}employee`;

    constructor(private http: HttpClient) { }

    getAllEmployees(): Observable<Employee[]> {
        return this.http.get<Employee[]>(this.apiUrl);
    }

    getEmployeeById(id: number): Observable<Employee> {
        return this.http.get<Employee>(`${this.apiUrl}/${id}`);
    }

    createEmployee(employee: Employee): Observable<Employee> {
        return this.http.post<Employee>(this.apiUrl, employee);
    }

    updateEmployee(id: number, employee: Employee): Observable<Employee> {
        return this.http.put<Employee>(`${this.apiUrl}/${id}`, employee);
    }

    deleteEmployee(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }

}
