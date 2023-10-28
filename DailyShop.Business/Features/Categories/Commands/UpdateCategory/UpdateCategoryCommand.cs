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

namespace DailyShop.Business.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand:IRequest<UpdateCategoryViewModel>
    {
        public UpdatedCategoryDto UpdatedCategoryDto { get; set; }
        public int Id { get; set; }
        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryViewModel>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<UpdateCategoryViewModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var mappedCategory = _mapper.Map<Category>(request.UpdatedCategoryDto);
                mappedCategory.Id = request.Id;
                var updatedCategory = await _categoryRepository.UpdateAsync(mappedCategory);
                UpdateCategoryViewModel updateCategoryViewModel = _mapper.Map<UpdateCategoryViewModel>(updatedCategory);
                return updateCategoryViewModel;
            }
        }
    }
}
