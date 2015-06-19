using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using Core.Common.Exceptions;

namespace Bom.Business.Managers
{
    public class ManagerBase
    {
        public ManagerBase()
        {
            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                var message = new StringBuilder(ex.Message);
                //ToDo refactor this
                if (ex.InnerException != null)
                {
                    message.AppendFormat(" InnerException: {0}", ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        message.AppendFormat(" InnerException: {0}", ex.InnerException.InnerException.Message);
                    }
                }
                throw new FaultException(message.ToString());
            }
        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
