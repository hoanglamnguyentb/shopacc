using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DepositRepository;
using Hinet.Service.DepositService.Dto;
using Hinet.Service.Common;
using System.Linq.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using AutoMapper;
using Hinet.Service.Constant;
using Hinet.Repository.AppUserRepository;




namespace Hinet.Service.DepositService
{
    public class DepositService : EntityService<Deposit>, IDepositService
    {
        IUnitOfWork _unitOfWork;
        IDepositRepository _DepositRepository;
	    ILog _loger;
        IMapper _mapper;
        IAppUserRepository _appUserRepository;

        public DepositService(IUnitOfWork unitOfWork,
            IDepositRepository DepositRepository,
            ILog loger,
            IMapper mapper,
            IAppUserRepository appUserRepository)
        : base(unitOfWork, DepositRepository)
        {
            _unitOfWork = unitOfWork;
            _DepositRepository = DepositRepository;
            _loger = loger;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
        }

        public PageListResultBO<DepositDto> GetDaTaByPage(DepositSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from Deposittbl in _DepositRepository.GetAllAsQueryable()

                        select new DepositDto
                        {
							UserId = Deposittbl.UserId,
							Code = Deposittbl.Code,
							Amount = Deposittbl.Amount,
							Status = Deposittbl.Status,
							Expiry = Deposittbl.Expiry
                            
                        };

            if (searchModel != null)
            {
		if (searchModel.UserIdFilter!=null)
		{
			query = query.Where(x => x.UserId==searchModel.UserIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.CodeFilter))
		{
			query = query.Where(x => x.Code.Contains(searchModel.CodeFilter));
		}
		if (searchModel.AmountFilter!=null)
		{
			query = query.Where(x => x.Amount==searchModel.AmountFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.StatusFilter))
		{
			query = query.Where(x => x.Status.Contains(searchModel.StatusFilter));
		}
		if (searchModel.ExpiryFilter!=null)
		{
			query = query.Where(x => x.Expiry==searchModel.ExpiryFilter);
		}


                if (!string.IsNullOrEmpty(searchModel.sortQuery))
                {
                    query = query.OrderBy(searchModel.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.Id);
            }
            var resultmodel = new PageListResultBO<DepositDto>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = dataPageList;
            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = dataPageList.ToList();
            }
            return resultmodel;
        }

        public Deposit GetById(long id)
        {
            return _DepositRepository.GetById(id);
        }
       
        public List<DepositDto> GetAll(long? id = null)
        {
            var depositQuery = _DepositRepository.GetAllAsQueryable();
            if (id.HasValue)
            {
                depositQuery = depositQuery.Where(x => x.UserId == id);
            }
            var query = from d in depositQuery
                        join u in _appUserRepository.GetAllAsQueryable()
                        on d.UserId equals u.Id into uGroup
                        from u in uGroup.DefaultIfEmpty()
                        select new DepositDto
                        {
                            Id = d.Id,
                            UserId = d.UserId,
                            Code = d.Code,
                            Amount = d.Amount,
                            Status = d.Status,
                            Expiry = d.Expiry,
                            UserName = u.UserName,
                            CreatedDate = d.CreatedDate
                        };
            var result = query.OrderByDescending(x => x.Id).ToList();
            var now =   DateTime.Now;
            foreach (var item in result)
            {
                if(item.Expiry <= now && item.Status != DepositConstant.SUCCESS)
                {
                    item.Status = DepositConstant.EXPIRED;
                }
            }
            return result;
        }
    

    }
}
