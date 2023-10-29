using AutoMapper;
using DailyShop.Business.Features.WebSiteSettings.Dtos;
using DailyShop.Business.Features.WebSiteSettings.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.WebSiteSettings.Commands.UpdateWebSiteSetting
{
    public class UpdateWebSiteSettingCommand:IRequest<string>
    {
        public UpdatedWebSiteSettingDto UpdatedWebSiteSettingDto { get; set; }
        public class UpdateWebSiteSettingCommandHandler : IRequestHandler<UpdateWebSiteSettingCommand,string>
        {
            private readonly IWebSiteSettingRepository _webSiteSettingRepository;
            private readonly IMapper _mapper;

            public UpdateWebSiteSettingCommandHandler(IWebSiteSettingRepository webSiteSettingRepository, IMapper mapper)
            {
                _webSiteSettingRepository = webSiteSettingRepository;
                _mapper = mapper;
            }

            public async Task<string> Handle(UpdateWebSiteSettingCommand request, CancellationToken cancellationToken)
            {
                var webSiteSetting = _mapper.Map<WebSiteSetting>(request.UpdatedWebSiteSettingDto);
                var findWebSiteSetting = await _webSiteSettingRepository.Query().FirstOrDefaultAsync();
                webSiteSetting.Id = findWebSiteSetting.Id;
                await _webSiteSettingRepository.UpdateAsync(webSiteSetting);

                return "Web site ayarlarınız başarıyla güncellendi.";
            }
        }
    }
}
