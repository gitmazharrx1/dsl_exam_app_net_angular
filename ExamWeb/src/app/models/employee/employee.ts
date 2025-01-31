import { CountryGridModel } from "../country/country-grid-model";

export class Employee {
    id: number | undefined;
    firstName: string | undefined;
    middleName?: string | undefined;
    lastName: string | undefined;
    phone: string | undefined;
    email: string | undefined;
    countryId?: number;
    countryName?: string | undefined;
    createdById: string | undefined;
    createdDateTime?: Date;
    updatedById?: string;
    updatedDateTime?: Date;
    isDeleted: boolean | undefined;
    deletedDateTime?: Date;
}
