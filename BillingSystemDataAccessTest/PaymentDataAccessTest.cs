using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataAccess;
using BillingSystemDataModel;

namespace BillingSystemDataAccessTest
{
    public class PaymentDataAccessTest
    {

       public void TestGetPaymentById(PaymentDataAccess paymentDataAccess)
        {
            Console.WriteLine("Testing GetPaymentById:");

            // Assuming there's a Payment with Id = 1 in the database
            var payment = paymentDataAccess.GetPaymentById(1);

            if (payment != null)
            {
                Console.WriteLine($"Payment found: Id = {payment.PaymentId}, Amount = {payment.Amount}");
            }
            else
            {
                Console.WriteLine("Payment not found.");
            }
        }

       public void TestGetAllPayments(PaymentDataAccess paymentDataAccess)
        {
            Console.WriteLine("\nTesting GetAllPayments:");

            var payments = paymentDataAccess.GetAllPayments();

            if (payments.Count > 0)
            {
                Console.WriteLine("Payments found:");
                foreach (var payment in payments)
                {
                    Console.WriteLine($"Id = {payment.PaymentId}, Amount = {payment.Amount}");
                }
            }
            else
            {
                Console.WriteLine("No Payments found.");
            }
        }

       public void TestAddPayment(PaymentDataAccess paymentDataAccess)
        {
            Console.WriteLine("\nTesting AddPayment:");

            // Create a new Payment object
            var newPayment = new Payment
            {
                PaymentMethod = "Credit Card",
                PaymentFrom = "John Doe",
                Amount = 500.0,
                BillAccountNumber = "BA123456",
                PaymentDate = DateTime.Now,
                InvoiceNumber = "INV789",
                PaymentStatus = "Completed",
                PaymentReference = "REF123456",
                BillAccountId = 3 // Assuming BillAccountId exists in the database
                // Add other properties as needed
            };

            // Add the new Payment
            paymentDataAccess.AddPayment(newPayment);
            Console.WriteLine("Payment added successfully.");
        }

        public void TestUpdatePayment(PaymentDataAccess paymentDataAccess)
        {
            Console.WriteLine("\nTesting UpdatePayment:");

            // Get an existing Payment by Id
            var payment = paymentDataAccess.GetPaymentById(1);

            if (payment != null)
            {
                // Update Payment properties
                payment.PaymentStatus = "Pending";

                // Update the Payment
                paymentDataAccess.UpdatePayment(payment);
                Console.WriteLine("Payment updated successfully.");
            }
            else
            {
                Console.WriteLine("Payment not found.");
            }
        }

       public void TestDeletePayment(PaymentDataAccess paymentDataAccess)
        {
            Console.WriteLine("\nTesting DeletePayment:");

            // Assuming there's a Payment with Id = 1 in the database
            paymentDataAccess.DeletePayment(1);
            Console.WriteLine("Payment deleted successfully.");
        }
    }
}
