using AutoMapper;
using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Queries.GetListReviewByUserId
{
    public class GetListReviewByUserIdQuery:IRequest<List<GetListReviewByUserIdDto>>
    {
        public int UserId { get; set; }
        public class GetListReviewByUserIdQueryHandler : IRequestHandler<GetListReviewByUserIdQuery, List<GetListReviewByUserIdDto>>
        {
            private readonly IReviewRepository _reviewRepository;
            private readonly IMapper _mapper;

            public GetListReviewByUserIdQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
            {
                _reviewRepository = reviewRepository;
                _mapper = mapper;
            }

            public async Task<List<GetListReviewByUserIdDto>> Handle(GetListReviewByUserIdQuery request, CancellationToken cancellationToken)
            {
                List<Review> reviews = await _reviewRepository.Query().Where(u => u.AppUser.Id == request.UserId).Include(a => a.AppUser).Include(p => p.Product).ToListAsync();
                List<GetListReviewByUserIdDto> mappedReviewDto = _mapper.Map<List<GetListReviewByUserIdDto>>(reviews);
                return mappedReviewDto;
            }
        }
    }
}
