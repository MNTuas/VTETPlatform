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
    public interface IWatchBusiness
    {
        Task<IBusinessResult> Save(Watch watch);
        Task<IBusinessResult> Update(Watch watch);
        Task<IBusinessResult> Delete(int watchID);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int watchid);
        Task<IBusinessResult> GetByIdAsync(int watchid);
    }
    public class watchBusiness : IWatchBusiness
    {
        //private readonly watchDAO _DAO;

        private readonly UnitOfWork _unitOfWork;
        public watchBusiness()
        {
            //neu no null moi tao => tiet kiem bo nho　
            _unitOfWork ??= new UnitOfWork();
        }



        public async Task<IBusinessResult> Save(Watch watch)
        {
            try
            {
                watch.Status = "Pending";

                int result = await _unitOfWork.WatchRepository.CreateAsync(watch);

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

        public async Task<IBusinessResult> Update(Watch watch)
        {
            try
            {

                int result = await _unitOfWork.WatchRepository.UpdateAsync(watch);
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

        public async Task<IBusinessResult> Delete(int watchID)
        {
            try
            {
                var watch = await _unitOfWork.WatchRepository.GetByIdAsync(watchID);
                if (watch != null)
                {
                    var result = await _unitOfWork.WatchRepository.RemoveAsync(watch);
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
                var watch = await _unitOfWork.WatchRepository.GetAllAsync();
                if (watch == null || !watch.Any())
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

        public async Task<IBusinessResult> GetById(int watchid)
        {
            try
            {
                var watch = await _unitOfWork.WatchRepository.GetByIdAsync(watchid);
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

        public async Task<IBusinessResult> GetByIdAsync(int watchid)
        {
            try
            {
                var watch = await _unitOfWork.WatchRepository.FirstOrDefaultAsync(m => m.Id == watchid);
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
