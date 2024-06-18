import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { InquilinoAppComponent } from './inquilino.app.component';
import { NovoComponent } from './novo/novo.component';


const inquilinoRouterConfig: Routes = [
    {
        path: '', component: InquilinoAppComponent,
        children: [
            //{ path: 'listar-todos', component: ListaComponent },
            {
                path: 'adicionar-novo', component: NovoComponent,
                //canDeactivate: [FornececedorGuard],
                //canActivate: [FornececedorGuard],
                data: [{ claim: { nome: 'Inquilino', valor: 'Adicionar'}}]
            }

        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(inquilinoRouterConfig)
    ],
    exports: [RouterModule]
})
export class InquilinoRoutingModule { }
