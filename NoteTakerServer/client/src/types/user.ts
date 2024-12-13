export interface User {
    userId?: string;
    userName: string;
    name: string;
    email: string;
    dateOfBirth: Date;
    password: string;
    accessToken?: string;
    expireTime: Date;
}