using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Features.Reviews.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Queries.GetListReviewByReport
{
    public class GetListReviewByReportQuery : IRequest<List<GetListReviewByReportViewModel>>
    {
        public class GetListReviewByReportQueryHandler : IRequestHandler<GetListReviewByReportQuery, List<GetListReviewByReportViewModel>>
        {
            private readonly IReportReviewRepository _reportReviewRepository;
            private readonly IMapper _mapper;
            public GetListReviewByReportQueryHandler(IReportReviewRepository reportReviewRepository, IMapper mapper)
            {
                _reportReviewRepository = reportReviewRepository;
                _mapper = mapper;
            }
            public async Task<List<GetListReviewByReportViewModel>> Handle(GetListReviewByReportQuery request, CancellationToken cancellationToken)
            {
                var reportedReviews = await _reportReviewRepository.Query().Include(x => x.ReporterUser).Include(x => x.Review).ThenInclude(x => x.AppUser).ToListAsync();
                if (reportedReviews == null)
                    throw new BusinessException("Raporlanan yorum bulunamadı.");
                var mappedReportedReviews = _mapper.Map<List<GetListReviewByReportViewModel>>(reportedReviews);
                int counter = 0;
                foreach (var item in reportedReviews)
                {
                    mappedReportedReviews[counter].Review.Id = item.Review.Id;
                    mappedReportedReviews[counter].Review.Description = item.Review.Description;
                    mappedReportedReviews[counter].Review.Status = item.Review.Status;
                    mappedReportedReviews[counter].Review.User.FirstName = item.Review.AppUser.FirstName;
                    mappedReportedReviews[counter].Review.User.LastName = item.Review.AppUser.LastName;
                    mappedReportedReviews[counter].Review.User.Email = item.Review.AppUser.Email;
                    mappedReportedReviews[counter].Review.User.ProfileImage = item.Review.AppUser.ProfileImage;
                    mappedReportedReviews[counter].Review.User.Id = item.Review.AppUser.Id;
                    counter++;
                }
                return mappedReportedReviews;
            }
        }
    }
}
