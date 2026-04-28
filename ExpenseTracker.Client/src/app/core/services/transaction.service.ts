import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  CreateExpenseRequest,
  CreateIncomeRequest,
  Transaction,
} from '../models/transaction.model';

@Injectable({ providedIn: 'root' })
export class TransactionService {
  private http = inject(HttpClient);
  private readonly API = '/api/transactions';

  getAll(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.API}`);
  }

  createExpense(request: CreateExpenseRequest): Observable<string> {
    return this.http.post<string>(`${this.API}/expense`, request);
  }

  createIncome(request: CreateIncomeRequest): Observable<string> {
    return this.http.post<string>(`${this.API}/income`, request);
  }
}
