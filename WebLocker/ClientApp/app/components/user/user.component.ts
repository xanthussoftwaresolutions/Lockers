import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/index';
import { Status } from '../../_models/status';
import { UserService } from '../../_services/index';
import { ToastrService } from 'toastr-ng2';
import { InputTextModule, DataTableModule, ButtonModule, DialogModule } from 'primeng/primeng';

class UserInfo implements User {
    constructor(public userId?, public loginUserId?, public pin?, public firstName?, public lastName?, public phone?, public status?, public email?) { }
}


@Component({
    selector: 'user',
    templateUrl: './user.component.html'
})
export class UserComponent implements OnInit {

    private rowData: any[];
    displayDialog: boolean;
    displayDeleteDialog: boolean;
    newUser: boolean;
    user: User = new UserInfo();
    users: User[];
    public editUserId: any;
    public fullname: string;
    statusBind = Array<Status>();
    constructor(private userService: UserService, private toastrService: ToastrService) {

    }

    ngOnInit() {
        this.editUserId = 0;
        this.statusBind = [
            new Status(true, "Active"),
            new Status(false, "InActive")
        ];
        this.statusBind = Array<Status>();
        this.statusBind.push(new Status(true, 'Active'));
        this.statusBind.push(new Status(false, 'In-Active'));
        this.loadData();
    }

    loadData() {
        this.userService.getUsers()
            .subscribe(res => {
                this.rowData = res.result;
            });
    }

    showDialogToAdd() {
        this.newUser = true;
        this.editUserId = 0;
        this.user = new UserInfo();
        this.displayDialog = true;
    }


    showDialogToEdit(user: User) {
        this.newUser = false;
        this.user = new UserInfo();
        this.user.userId = user.userId;
        this.user.firstName = user.firstName;
        this.user.lastName = user.lastName;
        this.user.email = user.email;
        this.user.phone = user.phone;
        this.user.loginUserId = user.loginUserId;
        this.user.pin = user.pin;
        this.user.status = user.status;
        this.displayDialog = true;
    }

    onRowSelect(event) {
    }

    save() {
        this.userService.saveUser(this.user)
            .subscribe(response => {
                this.user.userId > 0 ? this.toastrService.success('Data updated Successfully') :
                    this.toastrService.success('Data inserted Successfully');
                this.loadData();
            });
        this.displayDialog = false;
    }

    cancel() {
        this.user = new UserInfo();
        this.displayDialog = false;
    }


    showDialogToDelete(user: User) {
        this.fullname = user.firstName + ' ' + user.lastName;
        this.editUserId = user.userId;
        this.displayDeleteDialog = true;
    }

    okDelete(isDeleteConfirm: boolean) {
        if (isDeleteConfirm) {
            this.userService.deleteUser(this.editUserId)
                .subscribe(response => {
                    this.editUserId = 0;
                    this.loadData();
                });
            this.toastrService.error('Data Deleted Successfully');
        }
        this.displayDeleteDialog = false;
    }
}