using BridgePay.WCF.Models;
using System.Collections.Generic;
using System.ServiceModel;

[ServiceContract]
public interface ITransactionService
{ 
    [OperationContract]
    List<Transactions> GetAllTransactions();

    [OperationContract]
    void PostTransaction(Transactions transaction);

    [OperationContract]
    void ChangeStatus(int id, int status);

    [OperationContract]
    List<Transactions> ApproveSelectedItems(List<int> Ids);

    [OperationContract]
    List<Transactions> ApproveAllPendings();
}
