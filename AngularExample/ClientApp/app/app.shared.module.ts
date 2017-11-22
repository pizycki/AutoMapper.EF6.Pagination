import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { CustomersList } from "./components/customers/customers.component";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CustomersList
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'customers', pathMatch: 'full' },
            { path: 'customers', component: CustomersList },
            { path: '**', redirectTo: 'customers' }
        ])
    ]
})
export class AppModuleShared {
}
