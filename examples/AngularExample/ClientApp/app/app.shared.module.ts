import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";

import { InfiniteScrollModule } from "ngx-infinite-scroll";

import { AppComponent } from "components/app/app.component";
import { NavMenuComponent } from "components/navmenu/navmenu.component";
import { Pager } from "components/pager/pager.component";
import { DirectorsComponent } from "components/people/directors/directors.component";
import { CustomersComponent } from "components/people/customers/customers.component";
import { EmployersComponent } from "components/people/employers/employers.component";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        Pager,

        DirectorsComponent,
        CustomersComponent,
        EmployersComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        InfiniteScrollModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "customers", pathMatch: "full" },
            { path: "customers", component: CustomersComponent },
            { path: "employers", component: EmployersComponent },
            { path: "directors", component: DirectorsComponent },
            { path: "**", redirectTo: "customers" }
        ])
    ]
})
export class AppModuleShared {
}
