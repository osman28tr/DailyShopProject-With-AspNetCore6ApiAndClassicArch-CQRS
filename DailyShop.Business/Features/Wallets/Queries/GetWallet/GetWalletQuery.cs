using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Wallets.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Wallets.Queries.GetWallet
{
    public class GetWalletQuery:IRequest<GetWalletViewModel>
    {
        public int UserId { get; set; }
        public class GetWalletQueryHandler : IRequestHandler<GetWalletQuery, GetWalletViewModel>
        {
            private readonly IWalletRepository _walletRepository;
            private readonly IMapper _mapper;
            public GetWalletQueryHandler(IWalletRepository walletRepository, IMapper mapper)
            {
                _walletRepository = walletRepository;
                _mapper = mapper;
            }
            public async Task<GetWalletViewModel> Handle(GetWalletQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var wallet = await _walletRepository.GetAsync(x => x.UserId == request.UserId);
                    if (wallet == null) { return null; }
                    var mappedWallet = _mapper.Map<GetWalletViewModel>(wallet);
                    return mappedWallet;
                }
                catch (Exception hata)
                {
                    throw new BusinessException(hata.Message);
                }
            }
        }

    }
}
