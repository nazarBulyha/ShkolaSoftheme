using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
    interface IMobileOperator
    {
        List<IMobileAccount> MobileAccounts { get; set; }
        CallLog CallLogs { get; set; }

        int CreateNumber();

        void TryReceiveSMS(object sender, AccountEventArgs e);

        void TryMakeCall(object sender, AccountEventArgs e);

        void EndCall(object sender, AccountEventArgs e);
    }
}