﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillingSystemDataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BillingSystemEDMContainer : DbContext
    {
        public BillingSystemEDMContainer()
            : base("name=BillingSystemEDMContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BillAccount> BillAccounts { get; set; }
        public virtual DbSet<BillAccountPolicy> BillAccountPolicies { get; set; }
        public virtual DbSet<BillingTransaction> BillingTransactions { get; set; }
        public virtual DbSet<InstallmentSummary> InstallmentSummaries { get; set; }
        public virtual DbSet<Installment> Installments { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceInstallment> InvoiceInstallments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
    }
}
