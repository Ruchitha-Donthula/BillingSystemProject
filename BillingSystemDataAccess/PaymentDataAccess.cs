﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystemDataModel;

namespace BillingSystemDataAccess
{
    public class PaymentDataAccess
    {
        private readonly BillingSystemEDMContainer _context;

        public PaymentDataAccess()
        {
            _context = new BillingSystemEDMContainer(); // Assuming you have a DbContext named BillingSystemContext
        }

        public Payment GetPaymentById(int id)
        {
            try
            {
                return _context.Payments.FirstOrDefault(p => p.PaymentId == id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while retrieving Payment by Id: " + ex.Message);
                return null;
            }
        }

        public List<Payment> GetAllPayments()
        {
            try
            {
                return _context.Payments.ToList();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while retrieving all Payments: " + ex.Message);
                return new List<Payment>();
            }
        }

        public void AddPayment(Payment payment)
        {
            try
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while adding Payment: " + ex.Message);
            }
        }

        public void UpdatePayment(Payment payment)
        {
            try
            {
                var existingPayment = _context.Payments.Find(payment.PaymentId);
                if (existingPayment != null)
                {
                    // Update properties
                    existingPayment.PaymentMethod = payment.PaymentMethod;
                    existingPayment.PaymentFrom = payment.PaymentFrom;
                    existingPayment.Amount = payment.Amount;
                    existingPayment.BillAccountNumber = payment.BillAccountNumber;
                    existingPayment.PaymentDate = payment.PaymentDate;
                    existingPayment.InvoiceNumber = payment.InvoiceNumber;
                    existingPayment.PaymentStatus = payment.PaymentStatus;
                    existingPayment.PaymentReference = payment.PaymentReference;
                    existingPayment.BillAccountId = payment.BillAccountId;
                    // Update other properties similarly

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while updating Payment: " + ex.Message);
            }
        }

        public void DeletePayment(int id)
        {
            try
            {
                var payment = _context.Payments.Find(id);
                if (payment != null)
                {
                    _context.Payments.Remove(payment);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine("Error occurred while deleting Payment: " + ex.Message);
            }
        }
    }
}