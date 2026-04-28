import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { TransactionService } from '../../core/services/transaction.service';
import { Transaction } from '../../core/models/transaction.model';
import { TransactionFormComponent } from '../transactions/components/transaction-form/transaction-form.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, CurrencyPipe, DatePipe, TransactionFormComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  private transactionService = inject(TransactionService);

  transactions = signal<Transaction[]>([]);
  isLoading    = signal(false);
  showForm     = signal(false);

  totalIncome = computed(() =>
    this.transactions()
      .filter(t => t.transactionType === 'Income')
      .reduce((sum, t) => sum + t.amount, 0)
  );

  totalExpenses = computed(() =>
    this.transactions()
      .filter(t => t.transactionType === 'Expense')
      .reduce((sum, t) => sum + t.amount, 0)
  );

  balance = computed(() => this.totalIncome() - this.totalExpenses());

  ngOnInit() {
    this.loadTransactions();
  }

  loadTransactions() {
    this.isLoading.set(true);
    this.transactionService.getAll().subscribe({
      next: (data) => {
        this.transactions.set(data);
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  onTransactionAdded() {
    this.showForm.set(false);
    this.loadTransactions();
  }

  toggleForm() {
    this.showForm.update(v => !v);
  }
}
