using DailyShop.Business.Services.Repositories.Dapper;
using DailyShop.DataAccess.Concrete.Dapper.Contexts;
using DailyShop.Entities.Concrete;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace DailyShop.DataAccess.Concrete.Dapper.Repositories
{
	public class DpProductRepository : IDpProductRepository
	{
		public NpgsqlConnection connection = new(Context.Connection());
		public async Task<List<string>> GetProductDetailColorByIdAsync(int productId)
		{
			var query = $"""
			                     SELECT
			                       "Colors"."Name"
			                     FROM
			                       "Products"
			                       LEFT JOIN "ProductColor" ON "Products"."Id" = "ProductColor"."ProductId"
			                       LEFT JOIN "Colors" ON "ProductColor"."ColorId" = "Colors"."Id"
			                     WHERE
			                       "Products"."Id" = {productId}
			                     """;

			return (await connection.QueryAsync<string>(query))
				.Where(x => x != null)
				.ToList();
		}

		public async Task<List<string>> GetProductDetailImageByIdAsync(int productId)
		{
			var query = $"""
			                 SELECT "ProductImages"."Name"
			                 FROM "Products"
			                 INNER JOIN "ProductImages" ON "Products"."Id" = "ProductImages"."ProductId"
			                 WHERE "Products"."Id" = {productId}
			             """;

			return (await connection.QueryAsync<string>(query))
				.Where(x => x != null)
				.ToList();

		}

		public async Task<Product> GetProductByIdAsync(int? productId)
		{
			return (await connection.QueryFirstAsync<Product>(
				$"""select * from "Products" where "Id" = {productId}"""));
		}

		public async Task<List<string>> GetProductDetailSizeByIdAsync(int productId)
		{
			return (await connection.QueryAsync<string>(
					$"""SELECT "Sizes"."Name" FROM "Products" LEFT JOIN "ProductSize" ON "Products"."Id" = "ProductSize"."ProductId" LEFT JOIN "Sizes" ON "ProductSize"."SizeId" = "Sizes"."Id" where "Products"."Id" = {productId}""")).Where(x => x != null)
				.ToList();
		}

		public async Task<string> GetProductDetailUserByIdAsync(int productId)
		{
			return await connection.QueryFirstAsync<string>(
				$"""select "Users"."FirstName", "Users"."LastName" as UserName from "Users" inner join "Products" on "Users"."Id" = "Products"."UserId" where "Products"."Id" = {productId}""");
		}

		public async Task<Category> GetProductDetailCategoryByIdAsync(int? categoryId)
		{
			return await connection.QueryFirstAsync<Category>(
				$"""select * from "Categories" where "Id" = {categoryId}""");
		}
	}
}
