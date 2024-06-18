import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControlName, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { NgBrazilValidators } from 'ng-brazil';
import { utilsBr } from 'js-brasil';

import { Inquilino } from '../models/inquilino';
import { InquilinoService } from '../services/inquilino.service';
import { StringUtils } from 'src/app/utils/string-utils';
import { FormBaseComponent } from 'src/app/base-components/form-base.component';

@Component({
  selector: 'app-novo',
  templateUrl: './novo.component.html'
})
export class NovoComponent extends FormBaseComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  inquilinoForm: FormGroup;
  inquilino: Inquilino = new Inquilino();

  textoDocumento: string = 'CPF (requerido)';

  MASKS = utilsBr.MASKS;
  formResult: string = '';

  constructor(private fb: FormBuilder,
    private inquilinoService: InquilinoService,
    private router: Router,
    private toastr: ToastrService) {

    super();

    this.validationMessages = {
      nome: {
        required: 'Informe o Nome',
      },
      dataDeNascimento: {
        required: 'Informe a data de nascimento',
      },
      cpf: {
        required: 'Informe o Cpf',
      },
      nomeDaEmpresaOndeTrabalha: {
        required: 'Informe o nome da empresa onde trabalha',
      },
      telefone: {
        required: 'Informe o telefone',
      }
    };

    super.configurarMensagensValidacaoBase(this.validationMessages);
  }

  ngOnInit() {

    this.inquilinoForm = this.fb.group({
      nome: ['', [Validators.required]],
      dataDeNascimento: ['', [Validators.required]],
      cpf: ['', [Validators.required]],
      nomeDaEmpresaOndeTrabalha: ['', [Validators.required]],
      telefone: ['', [Validators.required]],
    });

  }

  ngAfterViewInit(): void {

  }

  adicionarInquilino() {
    if (this.inquilinoForm.dirty && this.inquilinoForm.valid) {

      this.inquilino = Object.assign({}, this.inquilino, this.inquilinoForm.value);
      this.formResult = JSON.stringify(this.inquilino);

      this.inquilinoService.novoInquilino(this.inquilino)
        .subscribe(
          sucesso => { this.processarSucesso(sucesso) },
          falha => { this.processarFalha(falha) }
        );
    }
  }

  processarSucesso(response: any) {
    this.inquilinoForm.reset();
    this.errors = [];

    this.mudancasNaoSalvas = false;

    let toast = this.toastr.success('Inquilino cadastrado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/inquilinos/listar-todos']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }
}
