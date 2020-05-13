import { User, Company, Post } from '.';

export class Address {
    id: number;
    housenUmber: number;
    appartementBus: string;
    streetId: number;
    zipId: number;
    countryId: number;
    users: Array<User>;
    companies: Array<Company>;
    posts: Array<Post>;
}
