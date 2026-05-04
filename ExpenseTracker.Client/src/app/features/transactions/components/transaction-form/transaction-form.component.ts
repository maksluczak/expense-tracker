import { Component, inject, output } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../../../core/services/transaction.service';
import { ExpenseType } from '../../../../core/models/transaction.model';

@Component({
  selector: 'app-transaction-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './transaction-form.component.html',
  styleUrl: './transaction-form.component.scss'
})
export class TransactionFormComponent {
  formSubmitted = output<void>();
  cancelled = output<void>();

  private fb = inject(FormBuilder);
  private transactionService = inject(TransactionService);

  activeTab: 'expense' | 'income' = 'expense';
  isSubmitting = false;

  readonly expenseTypes: ExpenseType[] = [
    'Food', 'Clothing', 'Transport', 'Housing', 'Entertainment', 'Health', 'Other'
  ];

  readonly expenseLabels: Record<ExpenseType, string> = {
    Food: 'Jedzenie',
    Clothing: 'Ubrania',
    Transport: 'Transport',
    Housing: 'Mieszkanie',
    Entertainment: 'Rozrywka',
    Health: 'Zdrowie',
    Other: 'Inne',
  };

  form = this.fb.group({
    amount: [null as number | null, [Validators.required, Validators.min(0.01)]],
    description: ['', Validators.required],
    expenseType: ['Food' as ExpenseType],
    date: [new Date().toISOString().split('T')[0], Validators.required]
  });

  setTab(tab: 'expense' | 'income') {
    this.activeTab = tab;
  }

  isInvalid(field: string): boolean {
    const ctrl = this.form.get(field);
    return !!(ctrl?.invalid && ctrl?.touched);
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { amount, description, expenseType, date } = this.form.value;
    this.isSubmitting = true;

    const request$ = this.activeTab === 'expense'
      ? this.transactionService.createExpense({
        amount: amount!,
        description: description!,
        expenseType: expenseType as ExpenseType,
        date: date!
      })
      : this.transactionService.createIncome({
        amount: amount!,
        description: description!,
        date: date!
      });

    request$.subscribe({
      next: () => {
        this.isSubmitting = false;
        this.formSubmitted.emit();
      },
      error: () => {
        this.isSubmitting = false;
      }
    });
  }
}
