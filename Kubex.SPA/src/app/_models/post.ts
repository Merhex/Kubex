import { DailyActivityReport } from './dailyActivityReport';
import { Address } from './address';
import { Company } from './company';
import { User } from '.';

export class Post {
    id?: number;
    name: string;
    company: Company;
    address?: Address;
    location?: Location;
    dailyActivityReports?: DailyActivityReport[];
    users?: User[];
}
