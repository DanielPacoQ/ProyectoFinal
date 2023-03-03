using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication1.BridgePayServiceClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TransactionController : ApiController
    {
        private readonly TransactionServiceClient proxy = new TransactionServiceClient();

        [HttpGet]
        [Route("api/Transaction")]
        public Transactions[] GetAllTransactions()
        {
            return proxy.GetAllTransactionsAsync().Result;
        }

        [HttpPost]
        [Route("api/Transaction")]
        public void PostTransaction([FromBody] Transactions Transaction)
        {
            try
            {
                proxy.PostTransactionAsync(Transaction).Wait();
            }
            catch
            {

            }
        }

        [HttpPut]
        [Route("api/Transaction/{id}/{status}")]
        public void ChangeStatus(int id, int status)
        {
            try
            {
                proxy.ChangeStatusAsync(id, status).Wait();
            }
            catch
            {

            }
        }

        [HttpPut]
        [Route("api/Transaction/all")]
        public Transactions[] ApproveAllPendings()
        {
            return proxy.ApproveAllPendingsAsync().Result;

        }

        [HttpPut]
        [Route("api/Transaction/ids")]
        public Transactions[] ApproveSelectedItems([FromBody] Ids ids)
        {
            return proxy.ApproveSelectedItemsAsync(ids.idList).Result;
        }
    }
}