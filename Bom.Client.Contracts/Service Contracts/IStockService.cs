using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Entities;
using Core.Common.Contracts;

namespace Bom.Client.Contracts
{
    [ServiceContract]
    public interface IStockService : IServiceContract
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
