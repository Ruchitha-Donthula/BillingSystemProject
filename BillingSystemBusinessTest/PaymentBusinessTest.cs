﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataModel;
using BillingSystemBusiness;

namespace BillingSystemBusinessTest
{
    public class PaymentBusinessTest
    {
        public void TestApplyPayment()
        {
            Payment payment = new Payment
            {
                BillAccountId=1017,
                BillAccountNumber = "BA000006", 
                InvoiceNumber = "IN000005",
                PaymentMethod = ApplicationConstants.BILL_ACCOUNT_CASH_PAYMENT_METHOD, 
                PaymentFrom="Ganesh",
                PaymentDate=DateTime.Now.Date,
                Amount = 200.0,
                PaymentStatus="Success",
                PaymentReference="REF123460"
            };

            try
            {
                new PaymentBusiness().ApplyPayment(payment);

                Console.WriteLine("Payment applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to apply payment: " + ex.Message);
            }
        }
    }
}
