using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTET.Business.Base;
using VTET.Common;
using VTET.Data;
using VTET.Data.Models;


namespace VTET.Business
{
    public interface IEvaluationBusiness
    {
        Task<IBusinessResult> Save(Evaluation evaluation);
        Task<IBusinessResult> Update(Evaluation evaluation);
        Task<IBusinessResult> Delete(int evaluationID);
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int evaluationid);
        Task<IBusinessResult> GetByIdAsync(int evaluationid);
    }
    public class evaluationBusiness : IEvaluationBusiness
    {
        //private readonly evaluationDAO _DAO;

        private readonly UnitOfWork _unitOfWork;

        public evaluationBusiness()
        {
            //neu no null moi tao => tiet kiem bo nho　
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> Save(Evaluation evaluation)
        {
            try
            {

                int result = await _unitOfWork.EvaluationRepository.CreateAsync(evaluation);

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

        public async Task<IBusinessResult> Update(Evaluation evaluation)
        {
            try
            {

                int result = await _unitOfWork.EvaluationRepository.UpdateAsync(evaluation);
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

        public async Task<IBusinessResult> Delete(int evaluationID)
        {
            try
            {
                var evaluation = await _unitOfWork.EvaluationRepository.GetByIdAsync(evaluationID);
                if (evaluation != null)
                {
                    var result = await _unitOfWork.EvaluationRepository.RemoveAsync(evaluation);
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
                var evaluation = await _unitOfWork.EvaluationRepository.GetAllAsync();
                if (evaluation == null || !evaluation.Any())
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, evaluation);

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int evaluationid)
        {
            try
            {
                var evaluation = await _unitOfWork.EvaluationRepository.GetByIdAsync(evaluationid);
                if (evaluation == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, evaluation);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetByIdAsync(int evaluationid)
        {
            try
            {
                var watch = await _unitOfWork.EvaluationRepository.FirstOrDefaultAsync(m => m.Id == evaluationid);
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