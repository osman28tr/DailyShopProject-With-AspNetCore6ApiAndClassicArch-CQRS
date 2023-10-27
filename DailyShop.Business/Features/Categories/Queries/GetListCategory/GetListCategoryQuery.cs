using AutoMapper;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Queries.GetListCategory
{
    public class GetListCategoryQuery : IRequest<List<GetListCategoryDto>>
    {
        public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, List<GetListCategoryDto>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, IProductRepository productRepository)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
                _productRepository = productRepository;
            }

            public async Task<List<GetListCategoryDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                //List<int> categoryIds = await _categoryRepository.Query().Where(c => c.ParentCategoryId == null).Select(x => x.Id).ToListAsync();

                List<Category> categories = await _categoryRepository.Query().Include(c => c.SubCategories).ThenInclude(c => c.SubCategories).ThenInclude(c=>c.SubCategories).Where(c => c.ParentCategoryId == null).ToListAsync();

                //int depth = 5; // İstediğiniz derinlik seviyesini belirleyin
                //var query = _categoryRepository.Query();
                //query = IncludeSubCategories(query, depth);

                //List<Category> targetCategories = await query
                //    .Where(c => categoryIds.Contains(c.Id))
                //    .ToListAsync();

                List<GetListCategoryDto> mappedGetListCategory = _mapper.Map<List<GetListCategoryDto>>(categories);
                return mappedGetListCategory;
            }
        }
    }
}
