import { Component } from '@angular/core';
import { AuthGuard } from '../../_guards/index';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    LogIn: boolean = false;
    constructor( private authGuard: AuthGuard) { }
    ngOnInit() {
        this.LogIn = this.authGuard.canActivate();
    }
}
