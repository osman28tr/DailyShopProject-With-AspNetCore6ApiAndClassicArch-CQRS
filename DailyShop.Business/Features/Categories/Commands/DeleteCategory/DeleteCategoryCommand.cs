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

namespace DailyShop.Business.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand:IRequest<DeleteCategoryViewModel>
    {
        public DeletedCategoryDto DeletedCategoryDto { get; set; }
        public GetListCategoryDto GetListCategoryDto { get; set; }

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
                //var category = await _categoryRepository.GetAsync(x => x.Id == request.DeletedCategoryDto.Id);
                var category = _mapper.Map<Category>(request.GetListCategoryDto);

                if (category.SubCategories.Any())
                {
                    foreach (var subCategory in category.SubCategories)
                    {
                        subCategory.ParentCategoryId = null;
                        //var findCategory = await _categoryRepository.GetAsync(x=>x.Id==subCategory.Id);
                        // Alt kategoriyi güncelleyin
                        await _categoryRepository.UpdateAsync(subCategory);
                        await _categoryRepository.DeleteAsync(subCategory, true);
                    }
                }

                // Üst kategori ile olan ilişkiyi kesin
                if (category.ParentCategoryId != null)
                {
                    var parentCategory = await _categoryRepository.GetAsync(x => x.Id == category.ParentCategoryId);
                    parentCategory?.SubCategories?.Remove(category);
                    if (parentCategory != null) await _categoryRepository.UpdateAsync(parentCategory);
                }

                var deletedCategory = await _categoryRepository.DeleteAsync(category, true);
                var mappedCategory = _mapper.Map<DeleteCategoryViewModel>(deletedCategory);
                //await _categoryRepository.DeleteAsync(category);
                return mappedCategory;
            }
        }
    }
}
