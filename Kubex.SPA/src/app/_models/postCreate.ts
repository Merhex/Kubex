import { Address, Company } from './';
import { User } from './user';

export class PostCreate {
    id?: number;
    name: string;
    companyId: number;
    company?: Company;
    address?: Address;
    userNames?: string[];
}
