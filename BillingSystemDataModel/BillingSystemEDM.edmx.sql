
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/03/2024 14:55:26
-- Generated from EDMX file: C:\Users\Admin\source\repos\BillingSystem\BillingSystemDataModel\BillingSystemEDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BillingSystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BillAccounts'
CREATE TABLE [dbo].[BillAccounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BillAccountId] int  NOT NULL,
    [BillAccountNumber] nvarchar(max)  NOT NULL,
    [BillingType] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [PayorName] nvarchar(max)  NOT NULL,
    [PayorAddress] nvarchar(max)  NOT NULL,
    [PaymentMethod] nvarchar(max)  NOT NULL,
    [DueDay] int  NOT NULL,
    [AccountTotal] float  NULL,
    [AccountPaid] float  NULL,
    [AccountBalance] float  NULL,
    [LastPaymentDate] datetime  NULL,
    [LastPaymentAmount] float  NULL,
    [PastDue] float  NULL,
    [FutureDue] float  NULL
);
GO

-- Creating table 'BillAccountPolicies'
CREATE TABLE [dbo].[BillAccountPolicies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BillAccountPolicyId] int  NOT NULL,
    [PolicyNumber] nvarchar(max)  NOT NULL,
    [BillAccountId] int  NOT NULL
);
GO

-- Creating table 'BillingTransactions'
CREATE TABLE [dbo].[BillingTransactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BillingTransactionId] int  NOT NULL,
    [ActivityDate] datetime  NOT NULL,
    [TransactionType] nvarchar(max)  NOT NULL,
    [TransactionAmount] float  NULL,
    [InvoiceId] int  NULL,
    [PaymentId] int  NULL,
    [BillAccountId] int  NOT NULL
);
GO

-- Creating table 'InstallmentSummaries'
CREATE TABLE [dbo].[InstallmentSummaries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InstallmentSummaryId] int  NOT NULL,
    [PolicyNumber] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [BillAccountId] int  NOT NULL
);
GO

-- Creating table 'Installments'
CREATE TABLE [dbo].[Installments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InstallmentId] int  NOT NULL,
    [InstallmentSequenceNumber] int  NOT NULL,
    [InstallmentSendDate] datetime  NOT NULL,
    [InstallmentDueDate] datetime  NOT NULL,
    [DueAmount] float  NOT NULL,
    [PaidAmount] float  NULL,
    [BalanceAmount] float  NULL,
    [InvoiceStatus] nvarchar(max)  NULL,
    [InstallmentSummaryId] int  NOT NULL,
    [InvoiceId] int  NOT NULL
);
GO

-- Creating table 'Invoices'
CREATE TABLE [dbo].[Invoices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InvoiceId] int  NOT NULL,
    [InvoiceNumber] nvarchar(max)  NOT NULL,
    [InvoiceDate] datetime  NOT NULL,
    [SendDate] datetime  NOT NULL,
    [BillAccountId] int  NOT NULL,
    [Status] nvarchar(max)  NULL,
    [InvoiceAmount] float  NOT NULL
);
GO

-- Creating table 'InvoiceInstallments'
CREATE TABLE [dbo].[InvoiceInstallments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InvoiceInstallmentId] int  NOT NULL,
    [InvoiceId] int  NOT NULL,
    [InstallmentId] int  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PaymentId] int  NOT NULL,
    [PaymentMethod] nvarchar(max)  NOT NULL,
    [PaymentFrom] nvarchar(max)  NOT NULL,
    [Amount] float  NOT NULL,
    [BillAccountNumber] nvarchar(max)  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [InvoiceNumber] nvarchar(max)  NOT NULL,
    [PaymentStatus] nvarchar(max)  NOT NULL,
    [PaymentReference] nvarchar(max)  NOT NULL,
    [BillAccountId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BillAccounts'
ALTER TABLE [dbo].[BillAccounts]
ADD CONSTRAINT [PK_BillAccounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BillAccountPolicies'
ALTER TABLE [dbo].[BillAccountPolicies]
ADD CONSTRAINT [PK_BillAccountPolicies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BillingTransactions'
ALTER TABLE [dbo].[BillingTransactions]
ADD CONSTRAINT [PK_BillingTransactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InstallmentSummaries'
ALTER TABLE [dbo].[InstallmentSummaries]
ADD CONSTRAINT [PK_InstallmentSummaries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Installments'
ALTER TABLE [dbo].[Installments]
ADD CONSTRAINT [PK_Installments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [PK_Invoices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InvoiceInstallments'
ALTER TABLE [dbo].[InvoiceInstallments]
ADD CONSTRAINT [PK_InvoiceInstallments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BillAccountId] in table 'BillAccountPolicies'
ALTER TABLE [dbo].[BillAccountPolicies]
ADD CONSTRAINT [FK_BillAccountBillAccountPolicy]
    FOREIGN KEY ([BillAccountId])
    REFERENCES [dbo].[BillAccounts]
        ([Id])
    ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillAccountBillAccountPolicy'
CREATE INDEX [IX_FK_BillAccountBillAccountPolicy]
ON [dbo].[BillAccountPolicies]
    ([BillAccountId]);
GO

-- Creating foreign key on [BillAccountId] in table 'BillingTransactions'
ALTER TABLE [dbo].[BillingTransactions]
ADD CONSTRAINT [FK_BillAccountBillingTransaction]
    FOREIGN KEY ([BillAccountId])
    REFERENCES [dbo].[BillAccounts]
        ([Id])
     ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillAccountBillingTransaction'
CREATE INDEX [IX_FK_BillAccountBillingTransaction]
ON [dbo].[BillingTransactions]
    ([BillAccountId]);
GO

-- Creating foreign key on [BillAccountId] in table 'InstallmentSummaries'
ALTER TABLE [dbo].[InstallmentSummaries]
ADD CONSTRAINT [FK_BillAccountInstallmentSummary]
    FOREIGN KEY ([BillAccountId])
    REFERENCES [dbo].[BillAccounts]
        ([Id])
     ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillAccountInstallmentSummary'
CREATE INDEX [IX_FK_BillAccountInstallmentSummary]
ON [dbo].[InstallmentSummaries]
    ([BillAccountId]);
GO

-- Creating foreign key on [InstallmentSummaryId] in table 'Installments'
ALTER TABLE [dbo].[Installments]
ADD CONSTRAINT [FK_InstallmentSummaryInstallment]
    FOREIGN KEY ([InstallmentSummaryId])
    REFERENCES [dbo].[InstallmentSummaries]
        ([Id])
    ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstallmentSummaryInstallment'
CREATE INDEX [IX_FK_InstallmentSummaryInstallment]
ON [dbo].[Installments]
    ([InstallmentSummaryId]);
GO

-- Creating foreign key on [InvoiceId] in table 'Installments'
ALTER TABLE [dbo].[Installments]
ADD CONSTRAINT [FK_InvoiceInstallment1]
    FOREIGN KEY ([InvoiceId])
    REFERENCES [dbo].[Invoices]
        ([Id])
    ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceInstallment1'
CREATE INDEX [IX_FK_InvoiceInstallment1]
ON [dbo].[Installments]
    ([InvoiceId]);
GO

-- Creating foreign key on [BillAccountId] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [FK_BillAccountPayment]
    FOREIGN KEY ([BillAccountId])
    REFERENCES [dbo].[BillAccounts]
        ([Id])
     ON DELETE CASCADE ON UPDATE CASCADE;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillAccountPayment'
CREATE INDEX [IX_FK_BillAccountPayment]
ON [dbo].[Payments]
    ([BillAccountId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------