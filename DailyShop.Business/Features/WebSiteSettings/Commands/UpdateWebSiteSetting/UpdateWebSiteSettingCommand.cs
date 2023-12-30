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
        public UpdatedWebSiteSettingDto? UpdatedWebSiteSettingDto { get; set; }
        public string? SiteIconPath { get; set; }
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

                    //var mappedWebSiteSetting = _mapper.Map<WebSiteSetting>(request.UpdatedWebSiteSettingDto);
                    

                    if (webSiteSetting != null)
                    {
                        webSiteSetting.UpdatedAt = DateTime.Now;
                        webSiteSetting.Icon = request.SiteIconPath;
                        _mapper.Map(request.UpdatedWebSiteSettingDto, webSiteSetting);
                        await _webSiteSettingRepository.UpdateAsync(webSiteSetting);
                        return "Web site ayarlarınız başarıyla güncellendi.";
                    }
                    else
                    {
                        webSiteSetting.CreatedAt = DateTime.Now;
                        await _webSiteSettingRepository.AddAsync(webSiteSetting);
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
