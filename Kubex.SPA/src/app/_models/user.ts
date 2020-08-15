import { Address } from '.';


export class User {
    id?: number;
    userName: string;
    password?: string;
    firstName: string;
    lastName: string;
    address: Address;
    roles: Array<string>;
    token: string;
    isDeleting?: boolean;
    photoUrl: string;
}
