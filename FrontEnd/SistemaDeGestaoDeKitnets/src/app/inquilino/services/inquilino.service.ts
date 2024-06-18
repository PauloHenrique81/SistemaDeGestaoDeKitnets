import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";

import { BaseService } from 'src/app/services/base.service';
import { Inquilino } from '../models/inquilino';

@Injectable()
export class InquilinoService extends BaseService {

    inquilino: Inquilino = new Inquilino();

    constructor(private http: HttpClient) { super() }

    obterTodos(): Observable<Inquilino[]> {
        return this.http
            .get<Inquilino[]>(this.UrlServiceV1 + "inquilinos")
            .pipe(catchError(super.serviceError));
    }

    obterPorId(id: string): Observable<Inquilino> {
        return this.http
            .get<Inquilino>(this.UrlServiceV1 + "inquilinos/" + id, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    novoInquilino(inquilino: Inquilino): Observable<Inquilino> {
        return this.http
            .post(this.UrlServiceV1 + "inquilinos", inquilino, this.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    atualizarInquilino(inquilino: Inquilino): Observable<Inquilino> {
        return this.http
            .put(this.UrlServiceV1 + "inquilinos/" + inquilino.id, inquilino, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    excluirInquilino(id: string): Observable<Inquilino> {
        return this.http
            .delete(this.UrlServiceV1 + "inquilinos/" + id, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }



}
