﻿using System.ServiceModel;
using Bom.Business.Entities;
using Core.Common.Extensions;
using Bom.Data.Contracts.DTOs;

namespace Bom.Business.Contracts
{
    [ServiceContract]
    public interface IPartService
    {
        [OperationContract]
        Part[] GetAllParts();
        [OperationContract]
        HierarchyNode<ProductTree> GetProductTree();

        [OperationContract]
        Part GetPart(int id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Part UpdatePart(Part part);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePart(int partId);

        [OperationContract]
        void RecalculateCostsForAssembly(int partId);

        [OperationContract]
        void Recalculate(int partId, int productsNeeded);
    }
}
