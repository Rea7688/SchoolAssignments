using System;

namespace NovBank.BranchTransactionTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to NovBank Branch Transaction Tracker!");

            Console.Write("Enter the number of branches: ");
            int branches = int.Parse(Console.ReadLine());
            Console.Write("Enter the number of transactions per branch: ");
            int transactions = int.Parse(Console.ReadLine());
            Console.WriteLine();

            decimal[,] transactionData = new decimal[branches, transactions];

            // INPUT: Use 0-based indexing for array, display i+1 to user
            for (int i = 0; i < branches; i++)
            {
                Console.WriteLine($"Branch {i + 1}:");
                for (int j = 0; j < transactions; j++)
                {
                    Console.Write($"  Transaction {j + 1} amount: R ");

                    // FIX: Store the amount in the array
                    transactionData[i, j] = decimal.Parse(Console.ReadLine());

                    Console.WriteLine($"  Recorded transaction of R {transactionData[i, j]} for Branch {i + 1}");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      BRANCH TRANSACTION TOTALS");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine();

            // DISPLAY & ANALYSIS: Calculate totals per branch
            for (int i = 0; i < branches; i++)
            {
                decimal branchTotal = 0;

                Console.Write($"Branch {i + 1}: ");

                for (int j = 0; j < transactions; j++)
                {
                    Console.Write($"R {transactionData[i, j]}  ");
                    branchTotal += transactionData[i, j];
                }

                Console.WriteLine();
                Console.WriteLine($"  Total: R {branchTotal}");
                Console.WriteLine();
            }

            Console.WriteLine("All branches and transactions have been recorded successfully.");
        }
    }
}
