using AutoMapper;
using DailyShop.Business.Features.WebSiteSettings.Dtos;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.WebSiteSettings.Queries.GetWebSiteSetting
{
    public class GetWebSiteSettingQuery : IRequest<WebSiteSettingDto>
    {
        public class GetWebSiteSettingQueryHandler : IRequestHandler<GetWebSiteSettingQuery, WebSiteSettingDto>
        {
			private readonly IWebSiteSettingRepository _webSiteSettingRepository;
			private readonly IMapper _mapper;

			public GetWebSiteSettingQueryHandler(IWebSiteSettingRepository webSiteSettingRepository, IMapper mapper)
            {
				_webSiteSettingRepository = webSiteSettingRepository;
				_mapper = mapper;
			}

			public async Task<WebSiteSettingDto> Handle(GetWebSiteSettingQuery request, CancellationToken cancellationToken)
			{
				var webSiteSetting = await _webSiteSettingRepository.Query().FirstOrDefaultAsync();

				if (webSiteSetting != null)
				{
					var mappedWebSiteSetting = _mapper.Map<WebSiteSettingDto>(webSiteSetting);
					return mappedWebSiteSetting;
				}
				else
				{
					return null;
				}
			}
		}
    }
}
