using BridgePay.WCF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
public class TransactionService : ITransactionService, IDisposable
{
    BridgepayDBEntities context = new BridgepayDBEntities();
    public List<Transactions> GetAllTransactions() {
        return context.Transactions.ToList();
    }

    [OperationBehavior(TransactionScopeRequired = true)]
    public void PostTransaction(Transactions transaction)
    {
        context.Transactions.Add(transaction);
        context.SaveChanges();
    }

    public List<Transactions> ApproveSelectedItems(List<int> Ids)
    {
        foreach(int i in Ids)
        {
            context.SetToStatus(i,1);
        }
        context.SaveChanges();
        return context.Transactions.ToList();
    }
    public List<Transactions> ApproveAllPendings()
    {
        context.ApprovePendings();
        return context.Transactions.ToList();
    }

    public void ChangeStatus(int id, int status)
    {
        context.SetToStatus(id, status);
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
