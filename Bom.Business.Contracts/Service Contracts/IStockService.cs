using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;

namespace Bom.Business.Contracts
{
    [ServiceContract]
    public interface IStockService
    {
        [OperationContract]
        Stock[] GetAllStocks();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Stock UpdateStock(Stock stockItem);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStock(int stockItemId);
    }
}
