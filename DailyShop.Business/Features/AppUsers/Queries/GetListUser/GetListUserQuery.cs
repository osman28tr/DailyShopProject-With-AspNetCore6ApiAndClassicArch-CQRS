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
            private readonly IReviewRepository _reviewRepository;
            private readonly IAddressRepository _addressRepository;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IAppUserRepository appUserRepository, IMapper mapper, IAddressRepository addressRepository, IReviewRepository reviewRepository)
            {
                _appUserRepository = appUserRepository;
                _mapper = mapper;
                _addressRepository = addressRepository;
                _reviewRepository = reviewRepository;
            }

            public async Task<List<GetListUserDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                //belleğe alınan veriler 
                var addresses = await _addressRepository.Query().ToListAsync();
                var reviews = await _reviewRepository.Query().ToListAsync();

                var appUsers = await _appUserRepository.Query().ToListAsync(); // include olmadan joinli veriler bellekten getirildi.

                List<GetListUserDto> mappedUserDto = _mapper.Map<List<GetListUserDto>>(appUsers);
                int counter = 0;
                appUsers.ForEach(appUser =>
                {
                    if (appUser.Reviews != null && appUser.Reviews.Any())
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
