using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hinet.Model;
using Hinet.Repository;

using System.Linq.Expressions;
using System.Web.Mvc;
using Hinet.Model.Entities;
using Hinet.Service.Common;

namespace Hinet.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        IUnitOfWork _unitOfWork;
        IGenericRepository<T> _repository;
        private IUnitOfWork unitOfWork;
        ServiceResult<T> serviceResult;

        public BaseService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            serviceResult = new ServiceResult<T>();
        }

        protected BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Hàm lấy danh sách dữ liệu
        /// </summary>
        /// <returns></returns>
        public virtual ServiceResult<T> GetAllEntity()
        {
            var entity = _repository.GetAll();
            if (entity != null)
            {
                serviceResult.Status = true;
                serviceResult.Message = "Thành công";
                serviceResult.Data = (T)entity;
            }

            return serviceResult;
        }
        /// <summary>
        /// Hàm cất dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual ServiceResult<T> CreateEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var rowAffecteds = _repository.Add(entity);
            serviceResult.Status = true;
            serviceResult.Message = "Thành Công";
            serviceResult.Data = rowAffecteds;
            _unitOfWork.Commit();
            return serviceResult;
        }

       
    }
}
