export class Login {
    constructor(public grant_type: string, public username: string, public password: string, public refresh_token: string) { }
}