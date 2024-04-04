using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataAccess;

namespace BillingSystemDataAccessTest
{
    class BillingSystemDataAccessTestMain
    {
        public static void Main(String[] args)
        {
            var billAccountDataAccess = new BillAccountDataAccess();
            //new BillAccountDataAccessTest().TestAddBillAccount(billAccountDataAccess);
            //new BillAccountDataAccessTest().TestUpdateBillAccount(billAccountDataAccess);
            //new BillAccountDataAccessTest().TestDeleteBillAccount(billAccountDataAccess);
            //new BillAccountDataAccessTest().TestGetBillAccountById(billAccountDataAccess);
           // new BillAccountDataAccessTest().TestGetAllBillAccounts(billAccountDataAccess);

         
            var billAccountPolicyDataAccess = new BillAccountPolicyDataAccess();
            //new BillAccountPolicyDataAccessTest().TestAddBillAccountPolicy(billAccountPolicyDataAccess);
            //new BillAccountPolicyDataAccessTest().TestGetBillAccountPolicyById(billAccountPolicyDataAccess);
            //new BillAccountPolicyDataAccessTest().TestGetAllBillAccountPolicies(billAccountPolicyDataAccess);
            //new BillAccountPolicyDataAccessTest().TestDeleteBillAccountPolicy(billAccountPolicyDataAccess);

            
            var billingTransactionDataAccess = new BillingTransactionDataAccess();
            //new BillingTransactionDataAccessTest().TestAddBillingTransaction(billingTransactionDataAccess);
            //new BillingTransactionDataAccessTest().TestGetBillingTransactionById(billingTransactionDataAccess);
            //new BillingTransactionDataAccessTest().TestGetAllBillingTransactions(billingTransactionDataAccess);
            //new BillingTransactionDataAccessTest().TestDeleteBillingTransaction(billingTransactionDataAccess);

            /*
            var installmentDataAccess = new InstallmentDataAccess();
            new InstallmentDataAccessTest().TestAddInstallment(installmentDataAccess);
            new InstallmentDataAccessTest().TestGetInstallmentById(installmentDataAccess);
            new InstallmentDataAccessTest().TestUpdateInstallment(installmentDataAccess);
            new InstallmentDataAccessTest().TestDeleteInstallment(installmentDataAccess);
            */
            
            /*
            var installmentSummaryDataAccess = new InstallmentSummaryDataAccess();
            new InstallmentSummaryDataAccessTest().TestAddInstallmentSummary(installmentSummaryDataAccess);
            new InstallmentSummaryDataAccessTest().TestGetInstallmentSummaryById(installmentSummaryDataAccess);
            new InstallmentSummaryDataAccessTest().TestGetAllInstallmentSummaries(installmentSummaryDataAccess);
            new InstallmentSummaryDataAccessTest().TestUpdateInstallmentSummary(installmentSummaryDataAccess);
            new InstallmentSummaryDataAccessTest().TestDeleteInstallmentSummary(installmentSummaryDataAccess);
            */

            /*
            var invoiceDataAccess = new InvoiceDataAccess();
            new InvoiceDataAccessTest().TestAddInvoice(invoiceDataAccess);
            new InvoiceDataAccessTest().TestGetInvoiceById(invoiceDataAccess);
            new InvoiceDataAccessTest().TestGetAllInvoices(invoiceDataAccess);
            new InvoiceDataAccessTest().TestUpdateInvoice(invoiceDataAccess);
            new InvoiceDataAccessTest().TestDeleteInvoice(invoiceDataAccess);
            */

            /*
            var invoiceInstallmentDataAccess = new InvoiceInstallmentDataAccess();
            new InvoiceInstallmentDataAccessTest().TestAddInvoiceInstallment(invoiceInstallmentDataAccess);
            new InvoiceInstallmentDataAccessTest().TestGetInvoiceInstallmentById(invoiceInstallmentDataAccess);
            new InvoiceInstallmentDataAccessTest().TestGetAllInvoiceInstallments(invoiceInstallmentDataAccess);
            new InvoiceInstallmentDataAccessTest().TestDeleteInvoiceInstallment(invoiceInstallmentDataAccess);
            */

            
            var paymentDataAccess = new PaymentDataAccess();
            new PaymentDataAccessTest().TestAddPayment(paymentDataAccess);
            new PaymentDataAccessTest().TestGetPaymentById(paymentDataAccess);
            new PaymentDataAccessTest().TestGetAllPayments(paymentDataAccess);
            new PaymentDataAccessTest().TestUpdatePayment(paymentDataAccess);
            new PaymentDataAccessTest().TestDeletePayment(paymentDataAccess);

        }
    }
}
