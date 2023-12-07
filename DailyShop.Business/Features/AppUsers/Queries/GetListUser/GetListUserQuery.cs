using AutoMapper;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Queries.GetListUser
{
    public class GetListUserQuery : IRequest<List<GetListUserDto>>
    {
        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, List<GetListUserDto>>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IAppUserRepository appUserRepository, IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _mapper = mapper;
            }

            public async Task<List<GetListUserDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                List<AppUser> appUsers = await _appUserRepository.Query().Include(a => a.Addresses)
                    .Include(r => r.Reviews).ToListAsync();
                List<GetListUserDto> mappedUserDto = _mapper.Map<List<GetListUserDto>>(appUsers);
                int counter = 0;
                appUsers.ForEach(appUser =>
                {
                    if (appUser.Reviews.Any())
                    {
                        foreach (var userReview in appUser.Reviews)
                        {
                            GetListReviewByUserDto getListReviewByUserDto = new()
                            {
                                Name = userReview.Name,
                                Description = userReview.Description,
                                Avatar = userReview.Avatar,
                                Rating = userReview.Rating,
                                Status = userReview.Status,
                                CreatedAt = userReview.CreatedAt,
                                UpdatedAt = userReview.UpdatedAt,
                            };
                            mappedUserDto[counter].Reviews.Add(getListReviewByUserDto);
                        }
                    }
                    counter++;
                });
                return mappedUserDto;
            }
        }
    }
}
