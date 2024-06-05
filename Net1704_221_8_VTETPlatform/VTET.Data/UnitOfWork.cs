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
        private OrderDetailRepository _orderdetail;
        private OrderRepository _orderRepository;


        public UnitOfWork()
        {

        }

        public UnitOfWork(Net1704_221_8_VTETPlatformContext context)
        {
            _unitOfWorkContext = context;
        }

        public EvaluationRepository EvaluationRepository
        {
            get 
            {
                return _evaluationRepository ??= new Repository.EvaluationRepository();
            }
        }
        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                return _orderdetail ??= new Repository.OrderDetailRepository();
            }
        }
        public OrderRepository OrderRepository
        {
            get
            {
                return _orderRepository ??= new Repository.OrderRepository();
            }
        }



        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}
