import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Device } from '../_models/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

@Injectable()
export class DeviceService {

    private _getDevicesUrl = "/Device/GetDevices";
    public _saveUrl: string = '/Device/SaveDevice/';
    public _updateUrl: string = '/Device/UpdateDevice/';
    public _deleteByIdUrl: string = '/Device/DeleteDeviceByID/';
    private _getLockerssUrl = "/WebLocker/GetLockers";

    constructor(private http: Http) { }

    getDevices() {
        var headers = new Headers();
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var getDevicesUrl = this._getDevicesUrl;
        return this.http.get(getDevicesUrl, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }
    getLockers() {
        var headers = new Headers();
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var getLockerssUrl = this._getLockerssUrl;
        return this.http.get(getLockerssUrl, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }
    //Post
    saveDevice(device: Device): Observable<string> {
        let body = JSON.stringify(device);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this._saveUrl, body, options)
            .map(res => res.json().message)
            .catch(this.handleError);
    }

    //Delete
    deleteDevice(id: number): Observable<string> {
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