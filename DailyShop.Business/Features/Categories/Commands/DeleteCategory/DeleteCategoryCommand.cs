using AutoMapper;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Features.Categories.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand:IRequest<DeleteCategoryViewModel>
    {
        public DeletedCategoryDto DeletedCategoryDto { get; set; }
        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryViewModel>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<DeleteCategoryViewModel> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetAsync(x => x.Id == request.DeletedCategoryDto.Id);
                var deletedCategory = await _categoryRepository.DeleteAsync(category);
                var mappedCategory = _mapper.Map<DeleteCategoryViewModel>(deletedCategory);
                return mappedCategory;
            }
        }
    }
}
