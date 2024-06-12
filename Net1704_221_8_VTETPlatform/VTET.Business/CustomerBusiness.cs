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
    public class CustomerBusiness
    {
        public interface ICustomerBusiness
        {
            Task<IBusinessResult> Save(Customer customer);
            Task<IBusinessResult> Update(Customer customer);
            Task<IBusinessResult> Delete(int customerID);
            Task<IBusinessResult> GetAll();
            Task<IBusinessResult> GetById(int customerid);
            Task<IBusinessResult> Login(string email, string password);
        }
        public class customerBusiness : ICustomerBusiness
        {
            //private readonly evaluationDAO _DAO;

            private readonly UnitOfWork _unitOfWork;
            public customerBusiness()
            {
                //neu no null moi tao => tiet kiem bo nho　
                _unitOfWork ??= new UnitOfWork();
            }
            public async Task<IBusinessResult> Save(Customer customer)
            {
                try
                {


                    int result = await _unitOfWork.CustomerRepository.CreateAsync(customer);

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

            public async Task<IBusinessResult> Update(Customer customer)
            {
                try
                {

                    int result = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
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

            public async Task<IBusinessResult> Delete(int customerID)
            {
                try
                {
                    var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerID);
                    if (customer != null)
                    {
                        var result = await _unitOfWork.CustomerRepository.RemoveAsync(customer);
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
                    var customer = await _unitOfWork.CustomerRepository.GetAllAsync();
                    if (customer == null || !customer.Any())
                    {
                        return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);

                    }
                }
                catch (Exception ex)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }

            public async Task<IBusinessResult> GetById(int customerid)
            {
                try
                {
                    var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerid);
                    if (customer == null)
                    {
                        return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
                    }
                }
                catch (Exception ex)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }

            public async Task<IBusinessResult> Login(string email, string password) 
            {
                try
                {
                    var watch = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
                    
                    if (watch == null)
                    {
                        return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                    }
                    else
                    {

                        return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, watch);
                    }
                }
                catch (Exception ex)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }

        }
    }
}
