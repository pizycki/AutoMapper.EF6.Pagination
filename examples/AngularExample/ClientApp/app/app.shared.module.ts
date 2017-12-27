import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./components/app/app.component";
import { NavMenuComponent } from "./components/navmenu/navmenu.component";
import { CustomersFixed } from "./components/customers-fixed/customers-fixed.component";
import { Pager } from "./components/pager/pager.component";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CustomersFixed,
        Pager
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "customers", pathMatch: "full" },
            { path: "customers", component: CustomersFixed },
            { path: "**", redirectTo: "customers" }
        ])
    ]
})
export class AppModuleShared {
}
