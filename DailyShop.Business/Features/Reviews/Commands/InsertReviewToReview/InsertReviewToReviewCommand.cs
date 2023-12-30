using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Reviews.Commands.InsertReviewToReview
{
    public class InsertReviewToReviewCommand:IRequest
    {
        public InsertedReviewToReviewDto InsertedReviewToReview { get; set; }
        public int? AppUserId { get; set; }
        public int? ProductId { get; set; }
        public class InsertReviewToReviewCommandHandler : IRequestHandler<InsertReviewToReviewCommand>
        {
            private readonly IReviewRepository _reviewRepository;
            private readonly IMapper _mapper;
            public InsertReviewToReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper)
            {
                _reviewRepository = reviewRepository;
                _mapper = mapper;
            }
            public async Task Handle(InsertReviewToReviewCommand request, CancellationToken cancellationToken)
            {
                var mappedReview = _mapper.Map<Review>(request.InsertedReviewToReview);
                var parReview = await _reviewRepository.Query().FirstOrDefaultAsync(x => x.Id == request.InsertedReviewToReview.ParentReviewId, cancellationToken: cancellationToken);

                if (parReview != null) mappedReview.ParentReviewId = parReview.ParentReviewId ?? parReview.Id;
                if (request.AppUserId == null)
                    throw new BusinessException("Kullanıcı bulunamadı, sisteme tekrar giriş yapınız veya kayıt olmadıysanız kayıt olunuz.");
                mappedReview.AppUserId = request.AppUserId;
                mappedReview.ProductId = request.ProductId;
                mappedReview.Status = "New";
                await _reviewRepository.AddAsync(mappedReview);
            }
        }
    }

}
