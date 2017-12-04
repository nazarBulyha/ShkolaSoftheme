using System.Collections.Generic;

namespace MobileOperator
{
    abstract class IMobileOperator
    {
        public delegate void CallReceiverDelegate(IMobileAccount sourceAccount, IMobileAccount destinationAccount, IMobileOperator _operator);
        public event CallReceiverDelegate CallReceiver = null;


        public List<IMobileAccount> AccountList = new List<IMobileAccount>();
        RouteController _routeController;

        public string Prefix { get; set; } = "063";

        public virtual void StartCall(IMobileAccount sourceAccount, IMobileAccount destinationAccount, IMobileOperator _operator)
        {
            _routeController = new RouteController(sourceAccount, destinationAccount);
            //set up the connection
            _routeController.BeginRoute(AccountList, _operator);
            //make call if possible
            CallReceiver?.Invoke(sourceAccount, destinationAccount, _operator);
        }
    }
}