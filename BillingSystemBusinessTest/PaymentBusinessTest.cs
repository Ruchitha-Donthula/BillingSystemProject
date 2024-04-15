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
                BillAccountId=16,
                BillAccountNumber = "BA000001", 
                InvoiceNumber = "IN000001",
                PaymentMethod = "Credit Card", 
                PaymentFrom="Mahalaxmi",
                PaymentDate=DateTime.Now.Date,
                Amount = 174.0,
                PaymentStatus="Success",
                PaymentReference="REF123456"
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
