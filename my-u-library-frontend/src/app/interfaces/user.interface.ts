import { IRole } from "./role.interface";

export interface IUser {
    IdUser: number;
    FirstName: string;
    LastName: string;
    Email: string;
    IdRole: number;
    Role: IRole;
}