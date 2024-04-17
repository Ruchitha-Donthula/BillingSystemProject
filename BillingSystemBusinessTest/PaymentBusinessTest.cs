using System;
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
                BillAccountId=1015,
                BillAccountNumber = "BA000004", 
                InvoiceNumber = "IN000003",
                PaymentMethod = ApplicationConstants.BILL_ACCOUNT_CREDITCARD_PAYMENT_METHOD, 
                PaymentFrom="Mahii",
                PaymentDate=DateTime.Now.Date,
                Amount = 500.0,
                PaymentStatus="Success",
                PaymentReference="REF123458"
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
