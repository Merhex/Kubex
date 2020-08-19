import { Address } from '.';
import { Post } from './post';
export class User {
    id?: string;
    userName: string;
    password?: string;
    firstName: string;
    lastName: string;
    address: Address;
    roles: Array<string>;
    token: string;
    isDeleting?: boolean;
    photoUrl: string;
    postIds: Array<number>;
    posts: Post[];
}
