using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VTET.Data.Models;
using VTET.Data.Repository;


namespace VTET.Data
{
    public class UnitOfWork
    {
        private Net1704_221_8_VTETPlatformContext _unitOfWorkContext;
        private EvaluationRepository _evaluationRepository;

        public UnitOfWork()
        {

        }
        public EvaluationRepository EvaluationRepository
        {
            get 
            {
                return _evaluationRepository ??= new Repository.EvaluationRepository();
            }
        }
    }
}
