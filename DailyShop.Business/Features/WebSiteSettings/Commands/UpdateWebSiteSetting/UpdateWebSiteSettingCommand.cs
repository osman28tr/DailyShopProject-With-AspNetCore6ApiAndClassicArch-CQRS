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
using Core.CrossCuttingConcerns.Exceptions;

namespace DailyShop.Business.Features.WebSiteSettings.Commands.UpdateWebSiteSetting
{
    public class UpdateWebSiteSettingCommand:IRequest<string>
    {
        public UpdatedWebSiteSettingDto UpdatedWebSiteSettingDto { get; set; }
        public string SiteIconPath { get; set; }
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
                try
                {
                    var webSiteSetting = await _webSiteSettingRepository.Query().FirstOrDefaultAsync();

                    var mappedWebSiteSetting = _mapper.Map<WebSiteSetting>(request.UpdatedWebSiteSettingDto);
                    mappedWebSiteSetting.Icon = request.SiteIconPath;

                    if (webSiteSetting != null)
                    {
                        mappedWebSiteSetting.UpdatedAt = DateTime.Now;
                        mappedWebSiteSetting.Id = webSiteSetting.Id;
                        await _webSiteSettingRepository.UpdateAsync(mappedWebSiteSetting);
                        return "Web site ayarlarınız başarıyla güncellendi.";
                    }
                    else
                    {
                        mappedWebSiteSetting.CreatedAt = DateTime.Now;
                        await _webSiteSettingRepository.AddAsync(mappedWebSiteSetting);
                        return "Web site ayarlarınız başarıyla eklendi.";
                    }
                }
                catch (Exception hata)
                {
                    throw new BusinessException("Bir hata oluştu.");
                }
            }
        }
    }
}
