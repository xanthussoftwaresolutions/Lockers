import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Headers, RequestOptions, BaseRequestOptions } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'toastr-ng2';
//PRIMENG - Third party module
import { InputTextModule, DataTableModule, ButtonModule, DialogModule } from 'primeng/primeng';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { LoginComponent } from './components/login/login.component';
import { ContactComponent } from './components/contact/contact.component';
import { UserComponent } from './components/user/user.component';
import { DeviceComponent } from './components/device/device.component';

import { AuthGuard } from './_guards/index';

import { AuthenticationService, UserService } from './_services/index';
import { ContactService } from './_services/index';
import { DeviceService } from './_services/index';


class AppBaseRequestOptions extends BaseRequestOptions {
    headers: Headers = new Headers();
    constructor() {
        super();
        this.headers.append('Content-Type', 'application/json');
        this.body = '';
    }
}

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        LoginComponent,
        ContactComponent,
        UserComponent,
        DeviceComponent
    ],
    providers: [AuthGuard,AuthenticationService,ContactService, DeviceService, UserService,
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        { provide: RequestOptions, useClass: AppBaseRequestOptions }],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        InputTextModule, DataTableModule, ButtonModule, DialogModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'user', pathMatch: 'full' },
            //{ path: '', component: UserComponent, canActivate: [AuthGuard] },
            { path: 'contact', component: ContactComponent, canActivate: [AuthGuard] },
            { path: 'user', component: UserComponent, canActivate: [AuthGuard] },
            { path: 'device', component: DeviceComponent, canActivate: [AuthGuard] },
            { path: 'login', component: LoginComponent},
            { path: '**', redirectTo: 'contact' }
        ])
    ]
})
export class AppModuleShared {
}
