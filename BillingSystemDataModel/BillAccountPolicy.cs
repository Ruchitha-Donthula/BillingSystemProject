//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class BillAccountPolicy
    {
        public int Id { get; set; }
        public int BillAccountPolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public int BillAccountId { get; set; }
    
        public virtual BillAccount BillAccount { get; set; }
    }
}
