using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;

namespace DailyShop.Business.Features.Reviews.Commands.InsertReview
{
    public class InsertReviewCommand:IRequest
    {
        public InsertedReviewDto InsertedReviewDto { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public class InsertReviewCommandHandler:IRequestHandler<InsertReviewCommand>
        {
            private readonly IMapper _mapper;
            private readonly IReviewRepository _reviewRepository;
            public InsertReviewCommandHandler(IMapper mapper,IReviewRepository reviewRepository)
            {
                _mapper = mapper;
                _reviewRepository = reviewRepository;
            }
            public async Task Handle(InsertReviewCommand request, CancellationToken cancellationToken)
            {
                var addMappedReview = _mapper.Map<Review>(request.InsertedReviewDto);
                addMappedReview.ProductId = request.ProductId;
                addMappedReview.AppUserId = request.UserId;
                addMappedReview.Status = "Onay Bekliyor.";
                addMappedReview.CreatedAt = DateTime.Now;
                addMappedReview.UpdatedAt = DateTime.Now;
                await _reviewRepository.AddAsync(addMappedReview);
            }
        }
    }
}
