import { User } from './user';

export interface ExtendedUser {
    tokenString: string;
    user: User;
}
