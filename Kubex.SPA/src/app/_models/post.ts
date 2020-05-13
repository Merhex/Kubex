import { User, PostRole } from '.';

export class Post {
    id: number;
    name: string;
    companyId: number;
    addressId: number;
    locationId: number;
    users: Array<User>;
    roles: Array<PostRole>;
}
