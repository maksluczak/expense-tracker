export type ExpenseType = 'Food' | 'Transport' | 'Housing' | 'Entertainment' | 'Health' | 'Other';
export type TransactionType = 'Expense' | 'Income';

export interface Transaction {
  id: string;
  amount: number;
  description: string;
  transactionType: TransactionType;
  expenseType: ExpenseType | null;
  date: string;
}

export interface CreateExpenseRequest {
  amount: number;
  description: string;
  expenseType: ExpenseType;
  date: string;
}

export interface CreateIncomeRequest {
  amount: number;
  description: string;
  date: string;
}
