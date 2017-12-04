using System;
using System.Collections.Generic;
using System.Threading;

namespace MobileOperator
{
    class RouteController
    {
        private IMobileAccount _sourceAccount;
        private IMobileAccount _destinationAccount;

        public RouteController(IMobileAccount sourceAccount, IMobileAccount destinationAccount)
        {
            _sourceAccount = sourceAccount;
            _destinationAccount = destinationAccount;
        }

        public void BeginRoute(List<IMobileAccount> AccountList, IMobileOperator _operator)
        {
            bool numbersExist = AccountList.Contains(_sourceAccount) && AccountList.Contains(_destinationAccount);

            //try
            if(numbersExist)
            {
                _sourceAccount.IsBusy = true;
                _destinationAccount.IsBusy = true;

                Console.WriteLine($"{_sourceAccount.Number}, trying call to {_destinationAccount.Number}.");
                //Thread.Sleep(500);
                Console.WriteLine($"{ _destinationAccount.Number}, has answer the call.");
                //Thread.Sleep(1000);
                Console.WriteLine("Conversation has ended.");
                Console.WriteLine();
                Console.WriteLine();

                _sourceAccount.IsBusy = false;
                _destinationAccount.IsBusy = false;
            }
            else
            {
                Console.WriteLine("Number you are trying to deal is busy at the moment. Please try againe later.");
                //return false;
            }

            //Console.ReadKey();
        }
    }
}
