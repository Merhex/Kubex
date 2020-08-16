import { Address, Company } from './';

export class PostCreate {
    id?: number;
    name: string;
    companyId: number;
    company?: Company;
    address?: Address;
    users?: string[];
}
