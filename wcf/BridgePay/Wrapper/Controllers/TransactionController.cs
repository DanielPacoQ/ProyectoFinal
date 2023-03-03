using Microsoft.AspNetCore.Mvc;
using ServiceReference1;

namespace Wrapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly TransactionServiceClient proxy = new TransactionServiceClient();

        [HttpGet]
        public Transactions[] GetAllTransactions()
        {
            return proxy.GetAllTransactionsAsync().Result;
        }

        [HttpPost]
        public void PostTransaction(Transactions Transaction)
        {
            try
            {
                proxy.PostTransactionAsync(Transaction).Wait();
            }
            catch
            {

            }
        }

        [HttpPut("/approve/all")]
        public void ApproveAllPendings()
        {
            try
            {
                proxy.ApproveAllPendingsAsync().Wait();
            }
            catch
            {

            }
        }

        [HttpPut("/approve")]
        public void ApproveSelectedItems(int[] ids)
        {
            try
            {
                proxy.PutApproveSelectedItemsAsync(ids).Wait();
            }
            catch
            {

            }
        }
    }
}
