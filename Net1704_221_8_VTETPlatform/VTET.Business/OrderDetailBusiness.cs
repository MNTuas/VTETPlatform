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
    public interface IOrderDetailBusiness
    {
        Task<IBusinessResult> Save(OrderDetail orderDetail);
        Task<IBusinessResult> Update(OrderDetail orderDetail);
        Task<IBusinessResult> Delete(string orderDetailID);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string orderdetailid);
    }
    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public OrderDetailBusiness()
        {
            //neu no null => tao bo nho luu tru
            _unitOfWork ??= new UnitOfWork();
        }



        public async Task<IBusinessResult> Save(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.OrderDetailRepository.CreateAsync(orderDetail);
                /*                _unitOfWork.OrderDetailRepository.PrepareCreate(orderDetail);
                                _unitOfWork.OrderDetailRepository.SaveAsync();*/
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

        public async Task<IBusinessResult> Update(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.OrderDetailRepository.UpdateAsync(orderDetail);
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

        public async Task<IBusinessResult> Delete(string orderDetailID)
        {
            try
            {
                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(orderDetailID);
                if (orderDetail != null)
                {
                    var result = await _unitOfWork.OrderDetailRepository.RemoveAsync(orderDetail);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);

                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
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
                var orderDetail = await _unitOfWork.OrderDetailRepository.GetAllAsync();
                if (orderDetail == null || !orderDetail.Any())
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(string orderdetailid)
        {
            try
            {
                var orderdetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(orderdetailid);
                if (orderdetail == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderdetail);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
