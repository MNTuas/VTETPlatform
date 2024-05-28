using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTEATPlatform.Data.Repository;
using VTET.Data.Models;


namespace VTEATPlatform.Data
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
