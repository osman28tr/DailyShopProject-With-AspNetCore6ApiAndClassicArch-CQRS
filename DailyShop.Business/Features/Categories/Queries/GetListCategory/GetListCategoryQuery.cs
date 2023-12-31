﻿using AutoMapper;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                List<int> categoryIds = await _categoryRepository.Query().Where(c => c.ParentCategoryId == null).Select(x => x.Id).ToListAsync();

                var query = _categoryRepository.Query();
                var includes = new List<Expression<Func<Category, object>>>();

                // İhtiyaca göre Include ifadelerini ekleyin
                includes.Add(c => c.SubCategories);

                // Veritabanı sorgusu oluştururken Include ifadelerini uygulayın
                foreach (var includeExpression in includes)
                {
                    query = query.Include(includeExpression);
                }
                List<Category> targetCategories = query.ToList();
                query = query.Distinct();
               
                List<GetListCategoryDto> mappedGetListCategory = _mapper.Map<List<GetListCategoryDto>>(query);
                return mappedGetListCategory;
            }
            public IQueryable<Category> IncludeSubCategories(IQueryable<Category> query, int depth)
            {
                while(query.Any())
                {
                    query = query.Include(c=>c.SubCategories);
                    if (query.Include(c => c.SubCategories).ThenInclude(c => c.SubCategories).Any())
                    {
                        query = query.Include(c => c.SubCategories).ThenInclude(c => c.SubCategories);
                    }
                }
                return query;
            }
        }
    }
}
