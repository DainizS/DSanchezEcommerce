using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDepartamento" in both code and config file together.
    [ServiceContract]
    public interface IDepartamento
    {
        [OperationContract]
        SL_WCF.Result Add(ML.Departamento departamento);
        
        [OperationContract]
        [ServiceKnownType(typeof(ML.Departamento))] // especifica tipo de dato solo para select
        SL_WCF.Result GetAll();

        [OperationContract]
        SL_WCF.Result Update(ML.Departamento departamento);

        [OperationContract]
        SL_WCF.Result Delete(int IdDepartamento);

    }
}
