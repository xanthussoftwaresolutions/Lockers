
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { AuthenticationService } from './index';
import { User } from '../_models/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";


@Injectable()
export class UserService {

    private _getUsersUrl = "/WebLocker/GetUsers";
    public _saveUrl: string = '/WebLocker/SaveUser/';
    public _updateUrl: string = '/WebLocker/UpdateUser/';
    public _deleteByIdUrl: string = '/WebLocker/DeleteUserByID/';
    private _authenticationService: AuthenticationService;

    constructor(private http: Http, private authenticationService: AuthenticationService) {
    }

    getUsers() {
        var headers = new Headers({ 'Authorization': 'Bearer ' + this.authenticationService.token });
        //headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var getUsersUrl = this._getUsersUrl;
        return this.http.get(getUsersUrl, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }

    //Post
    saveUser(user: User): Observable<string> {
        let body = JSON.stringify(user);
        let headers = new Headers({ 'Content-Type': 'application/json' ,'Authorization': 'Bearer ' + this.authenticationService.token });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this._saveUrl, body, options)
            .map(res => res.json().message)
            .catch(this.handleError);
    }

    //Delete
    deleteUser(id: number): Observable<string> {
        //debugger
        var deleteByIdUrl = this._deleteByIdUrl + '/' + id

        return this.http.delete(deleteByIdUrl)
            .map(response => response.json().message)
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Opps!! Server error');
    }


}