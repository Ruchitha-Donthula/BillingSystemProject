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
                BillAccountId=10,
                BillAccountNumber = "BA000002", 
                InvoiceNumber = "IN000002",
                PaymentMethod = "Cash", 
                PaymentFrom="Mahesh",
                PaymentDate=DateTime.Now.Date,
                Amount = 25.0,
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
