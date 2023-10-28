using AutoMapper;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Features.Categories.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Commands.InsertCategory
{
    public class InsertCategoryCommand:IRequest<InsertCategoryViewModel>
    {
        public InsertedCategoryDto InsertedCategoryDto { get; set; }
        public class InsertCategoryCommandHandler : IRequestHandler<InsertCategoryCommand, InsertCategoryViewModel>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public InsertCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<InsertCategoryViewModel> Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
            {
                var mappedCategory = _mapper.Map<Category>(request.InsertedCategoryDto);
                var insertedCategory = await _categoryRepository.AddAsync(mappedCategory);
                InsertCategoryViewModel insertCategoryViewModel = _mapper.Map<InsertCategoryViewModel>(insertedCategory);
                return insertCategoryViewModel;
            }
        }
    }
}
