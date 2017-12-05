import { Component, OnInit } from '@angular/core';
import { Device } from '../../_models/index';
import { Status } from '../../_models/status';
import { DeviceService } from '../../_services/index';
import { ToastrService } from 'toastr-ng2';
import { InputTextModule, DataTableModule, ButtonModule, DialogModule } from 'primeng/primeng';

class DeviceInfo implements Device {
    constructor(public deviceId?, public id?, public deviceName?, public status?, public lockerId?) { }
}


@Component({
    selector: 'device',
    templateUrl: './device.component.html'
})
export class DeviceComponent implements OnInit {

    private rowData: any[];
    private lockerData: any[];
    displayDialog: boolean;
    displayDeleteDialog: boolean;
    newDevice: boolean;
    device: Device = new DeviceInfo();
    devices: Device[];
    public editDeviceId: any;
    public fullname: string;
    statusBind = Array<Status>();
    constructor(private deviceService: DeviceService, private toastrService: ToastrService) {

    }

    ngOnInit() {
        this.editDeviceId = 0;
        this.statusBind = [
            new Status(true, "Active"),
            new Status(false, "InActive")
        ];
        this.statusBind = Array<Status>();
        this.statusBind.push(new Status(true, 'Active'));
        this.statusBind.push(new Status(false, 'In-Active'));
        this.loadData();
        this.loadLockerData();
    }

    loadData() {
        this.deviceService.getDevices()
            .subscribe(res => {
                this.rowData = res.result;
            });
    }
    loadLockerData() {
        this.deviceService.getLockers()
            .subscribe(res => {
                this.lockerData = res.result;
            });
    }
    showDialogToAdd() {
        this.newDevice = true;
        this.editDeviceId = 0;
        this.device = new DeviceInfo();
        this.displayDialog = true;
    }


    showDialogToEdit(device: Device) {
        this.newDevice = false;
        this.device = new DeviceInfo();
        this.device.deviceId = device.deviceId;
        this.device.deviceName = device.deviceName;
        this.device.id = device.id;
        this.device.status = device.status;
        this.device.lockerId = device.lockerId;
        this.displayDialog = true;
    }

    onRowSelect(event) {
    }

    save() {
        this.deviceService.saveDevice(this.device)
            .subscribe(response => {
                this.device.id > 0 ? this.toastrService.success('Data updated Successfully') :
                    this.toastrService.success('Data inserted Successfully');
                this.loadData();
            });
        this.displayDialog = false;
    }

    cancel() {
        this.device = new DeviceInfo();
        this.displayDialog = false;
    }


    showDialogToDelete(device: Device) {
        this.fullname = device.deviceId;
        this.editDeviceId = device.id;
        this.displayDeleteDialog = true;
    }

    okDelete(isDeleteConfirm: boolean) {
        if (isDeleteConfirm) {
            this.deviceService.deleteDevice(this.editDeviceId)
                .subscribe(response => {
                    this.editDeviceId = 0;
                    this.loadData();
                });
            this.toastrService.error('Data Deleted Successfully');
        }
        this.displayDeleteDialog = false;
    }
}