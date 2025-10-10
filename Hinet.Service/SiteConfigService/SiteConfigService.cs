using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Repository;
using Hinet.Repository.SiteConfigRepository;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.SiteConfigService.Dto;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;




namespace Hinet.Service.SiteConfigService
{
    public class SiteConfigService : EntityService<SiteConfig>, ISiteConfigService
    {
        IUnitOfWork _unitOfWork;
        ISiteConfigRepository _SiteConfigRepository;
	ILog _loger;
        IMapper _mapper;


        
        public SiteConfigService(IUnitOfWork unitOfWork, 
		ISiteConfigRepository SiteConfigRepository, 
		        ILog loger,
            	IMapper mapper	
            )
            : base(unitOfWork, SiteConfigRepository)
        {
            _unitOfWork = unitOfWork;
            _SiteConfigRepository = SiteConfigRepository;
            _loger = loger;
            _mapper = mapper;
        }

        public PageListResultBO<SiteConfigDto> GetDaTaByPage(SiteConfigSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from SiteConfigtbl in _SiteConfigRepository.GetAllAsQueryable()

                        select new SiteConfigDto
                        {
							Description = SiteConfigtbl.Description,
							Keywords = SiteConfigtbl.Keywords,
							OgTitle = SiteConfigtbl.OgTitle,
							OgUrl = SiteConfigtbl.OgUrl,
							OgDescription = SiteConfigtbl.OgDescription,
							OgImage = SiteConfigtbl.OgImage,
							SiteTitle = SiteConfigtbl.SiteTitle,
							Favicon = SiteConfigtbl.Favicon,
							Logo = SiteConfigtbl.Logo,
                            KichHoat = SiteConfigtbl.KichHoat,
							CreatedDate = SiteConfigtbl.CreatedDate,
							CreatedBy = SiteConfigtbl.CreatedBy,
							CreatedID = SiteConfigtbl.CreatedID,
							UpdatedDate = SiteConfigtbl.UpdatedDate,
							UpdatedBy = SiteConfigtbl.UpdatedBy,
							UpdatedID = SiteConfigtbl.UpdatedID,
							IsDelete = SiteConfigtbl.IsDelete,
							DeleteTime = SiteConfigtbl.DeleteTime,
							DeleteId = SiteConfigtbl.DeleteId,
							Id = SiteConfigtbl.Id
                            
                        };

            if (searchModel != null)
            {
		if (!string.IsNullOrEmpty(searchModel.DescriptionFilter))
		{
			query = query.Where(x => x.Description.Contains(searchModel.DescriptionFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.KeywordsFilter))
		{
			query = query.Where(x => x.Keywords.Contains(searchModel.KeywordsFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.OgTitleFilter))
		{
			query = query.Where(x => x.OgTitle.Contains(searchModel.OgTitleFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.OgDescriptionFilter))
		{
			query = query.Where(x => x.OgDescription.Contains(searchModel.OgDescriptionFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.OgImageFilter))
		{
			query = query.Where(x => x.OgImage.Contains(searchModel.OgImageFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.SiteTitleFilter))
		{
			query = query.Where(x => x.SiteTitle.Contains(searchModel.SiteTitleFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.FaviconFilter))
		{
			query = query.Where(x => x.Favicon.Contains(searchModel.FaviconFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.LogoFilter))
		{
			query = query.Where(x => x.Logo.Contains(searchModel.LogoFilter));
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
            var resultmodel = new PageListResultBO<SiteConfigDto>();
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

        public SiteConfig GetById(long id)
        {
            return _SiteConfigRepository.GetById(id);
        }


        //[OutputCache(Duration = 31536000, VaryByParam = "none")]
        public SiteConfig GetActiveConfig()
        {
            var query = from SiteConfigtbl in _SiteConfigRepository.GetAllAsQueryable().Where(x => x.KichHoat == true)

                        select new SiteConfigDto
                        {
                            Description = SiteConfigtbl.Description,
                            Keywords = SiteConfigtbl.Keywords,
                            OgTitle = SiteConfigtbl.OgTitle,
                            OgUrl = SiteConfigtbl.OgUrl,
                            OgDescription = SiteConfigtbl.OgDescription,
                            OgImage = SiteConfigtbl.OgImage,
                            SiteTitle = SiteConfigtbl.SiteTitle,
                            Favicon = SiteConfigtbl.Favicon,
                            Logo = SiteConfigtbl.Logo,
                            KichHoat = SiteConfigtbl.KichHoat,
                            PrimaryColor = SiteConfigtbl.PrimaryColor,
                            SecondaryColor = SiteConfigtbl.SecondaryColor,
                            PrimaryHoverColor = SiteConfigtbl.PrimaryHoverColor,
                            TextTitleColor = SiteConfigtbl.TextTitleColor,
                            TextColor = SiteConfigtbl.TextColor,
                            LinkColor = SiteConfigtbl.LinkColor,
                            LinkHoverColor = SiteConfigtbl.LinkHoverColor,
                            LinkFacebook = SiteConfigtbl.LinkFacebook,
                            SoDienThoai = SiteConfigtbl.SoDienThoai,
                        };
            return query.FirstOrDefault();
        }

    }
}
