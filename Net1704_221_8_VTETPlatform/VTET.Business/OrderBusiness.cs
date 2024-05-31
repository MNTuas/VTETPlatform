using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTET.Business.Base;
using VTET.Common;
using VTET.Data.Models;
using VTET.Data;

namespace VTET.Business
{
    public interface IOrderBusiness
    {
        Task<IBusinessResult> Save(Order order);
        Task<IBusinessResult> Update(Order order);
        Task<IBusinessResult> Delete(int orderID);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int orderid);


    }

    public class OrderBusiness : IOrderBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public OrderBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> Save(Order order)
        {
            try
            {
                int result = await _unitOfWork.OrderRepository.CreateAsync(order);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }


        // Update an existing Order in the database
        public async Task<IBusinessResult> Update(Order order)
        {
            try
            {
                int result = await _unitOfWork.OrderRepository.UpdateAsync(order);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        // Delete an Order from the database by its ID
        public async Task<IBusinessResult> Delete(int orderID)
        {
            try
            {


                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderID);
                if (order != null)
                {
                    var result = await _unitOfWork.OrderRepository.RemoveAsync(order);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);

                    }
                }
                else
                {
                    return new BusinessResult(-1, "Order not found");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var order = await _unitOfWork.OrderRepository.GetAllAsync();

                if (order == null || !order.Any())
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int orderId)
        {
            try
            {
                #region Business rule
                #endregion

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }





    }
}
