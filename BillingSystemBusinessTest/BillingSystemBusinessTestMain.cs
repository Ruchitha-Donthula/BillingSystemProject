using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystemBusinessTest
{
    public class BillingSystemBusinessTestMain
    {
        public static void Main(string[] args)
        {
            //new BillAccountBusinessTest().TestCreateBillAccount();
            //new BillAccountBusinessTest().TestBillAccountPolicy();
            // new BillAccountBusinessTest().TestGetBillAccountById();
            //new BillAccountBusinessTest().TestGetBillAccountByNumber();
            //new BillAccountBusinessTest().TestUpdateBillAccount();
            //new BillAccountBusinessTest().TestSuspendBillAccount();
            //new BillAccountBusinessTest().TestReleaseBillAccount();

            new InstallmentBusinessTest().CreateInstallmentSchedules();
           //new InstallmentBusinessTest().TestInstallmentsInSummary();



        }
    }
}
