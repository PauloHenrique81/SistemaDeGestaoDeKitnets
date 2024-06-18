import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NovoComponent } from './novo/novo.component';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { InquilinoRoutingModule } from './inquilino.route';
import { InquilinoAppComponent } from './inquilino.app.component';

import { InquilinoService } from './services/inquilino.service';

import { NgBrazil } from 'ng-brazil';
import { NgxSpinnerModule } from "ngx-spinner";



@NgModule({
  declarations: [
    InquilinoAppComponent,
    NovoComponent
  ],
  imports: [
    CommonModule,
    InquilinoRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgBrazil,
    //TextMaskModule,
    NgxSpinnerModule
  ],
  providers: [
    InquilinoService,
  ]
})
export class InquilinoModule { }
