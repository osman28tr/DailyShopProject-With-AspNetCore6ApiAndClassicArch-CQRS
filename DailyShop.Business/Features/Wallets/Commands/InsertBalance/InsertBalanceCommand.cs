using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Wallets.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Wallets.Commands.InsertBalance
{
    public class InsertBalanceCommand : IRequest
    {
        public int UserId { get; set; }
        public InsertedBalanceDto InsertedBalanceDto { get; set; }
        public class InsertBalanceCommandHandler : IRequestHandler<InsertBalanceCommand>
        {
            private readonly IWalletRepository _walletRepository;
            public InsertBalanceCommandHandler(IWalletRepository walletRepository)
            {
                _walletRepository = walletRepository;
            }
            public async Task Handle(InsertBalanceCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var wallet = await _walletRepository.GetAsync(x => x.UserId == request.UserId);
                    if (wallet == null)
                    {
                        await _walletRepository.AddAsync(new Wallet() { UserId = request.UserId, Balance = request.InsertedBalanceDto.Balance });
                        return;
                    }
                    wallet.Balance = wallet.Balance + request.InsertedBalanceDto.Balance;
                    await _walletRepository.UpdateAsync(wallet);
                }
                catch (Exception hata)
                {
                    throw new BusinessException(hata.Message);
                }
            }
        }
    }
}
