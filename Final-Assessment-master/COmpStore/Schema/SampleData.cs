using COmpStore.Schema.Entities;
using COmpStore.Schema.IdentityEntities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace COmpStore.Schema
{
    public static class SampleData
    {
        const string defaultAdminUserName = "DefaultAdminUserName";
        const string defaultAdminPassword = "DefaultAdminPassword";

        public static async Task InitializeMusicStoreDatabaseAsync(IServiceProvider serviceProvider, bool createUsers = true)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<StoreDbContext>();

                if (await db.Database.EnsureCreatedAsync())
                {
                    await InsertTestData(scopeServiceProvider);
                    if (createUsers)
                    {
                        //await CreateAdminUser(scopeServiceProvider);
                    }
                }
            }
        }

        private static async Task InsertTestData(IServiceProvider serviceProvider)
        {
            var publishers = Publishers;
            var categories = Categories;
            var subCategories = SubCategories;

            var products = GetProducts();

            await AddOrUpdateAsync(serviceProvider, p => p.Id, Publishers.Select(publisher => publisher.Value));
            await AddOrUpdateAsync(serviceProvider, c => c.Id, Categories.Select(category => category.Value));
            await AddOrUpdateAsync(serviceProvider, sc => sc.Id, SubCategories.Select(subCategory => subCategory.Value));
            await AddOrUpdateAsync(serviceProvider, p => p.Id, products);
        }

        // TODO [EF] This may be replaced by a first class mechanism in EF
        private static async Task AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider,
            Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            // Query in a separate context so that we can attach existing entities as modified
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<StoreDbContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<StoreDbContext>();
                foreach (var item in entities)
                {
                    db.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Creates a store manager user who can manage the inventory.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            //const string adminRole = "Administrator";

            var userManager = serviceProvider.GetService<UserManager<User>>();
            // TODO: Identity SQL does not support roles yet
            //var roleManager = serviceProvider.GetService<ApplicationRoleManager>();
            //if (!await roleManager.RoleExistsAsync(adminRole))
            //{
            //    await roleManager.CreateAsync(new IdentityRole(adminRole));
            //}

            var user = await userManager.FindByNameAsync(configuration[defaultAdminUserName]);
            if (user == null)
            {
                user = new User { UserName = configuration[defaultAdminUserName] };
                await userManager.CreateAsync(user, configuration[defaultAdminPassword]);
                //await userManager.AddToRoleAsync(user, adminRole);
                await userManager.AddClaimAsync(user, new Claim("ManageStore", "Allowed"));
            }

            // NOTE: For end to end testing only
            var envPerfLab = configuration["PERF_LAB"];
            if (envPerfLab == "true")
            {
                for (int i = 0; i < 100; ++i)
                {
                    var email = string.Format("User{0:D3}@example.com", i);
                    var normalUser = await userManager.FindByEmailAsync(email);
                    if (normalUser == null)
                    {
                        await userManager.CreateAsync(new User { UserName = email, Email = email }, "Password~!1");
                    }
                }
            }
        }

        private static ProductEntity[] GetProducts()
        {
            var products = new ProductEntity[]
            {
                new ProductEntity { Description = "Description 1", Price = 1, MadeIn="China 1",ProductName="Product Name 1",Publisher=publishers["Publisher Name 1"], SubCategory=subCategories["SubCategory Name 1"] },
            };

            foreach (var product in products)
            {
                product.PublisherId = product.Publisher.Id;
                product.SubCategoryId = product.SubCategory.Id;
            }

            return products;
        }

        private static Dictionary<string, PublisherEntity> publishers;
        public static Dictionary<string, PublisherEntity> Publishers
        {
            get
            {
                if (publishers == null)
                {
                    var publishersList = new PublisherEntity[]
                    {
                        new PublisherEntity { PublisherName = "Publisher Name 1" },
                    };

                    publishers = new Dictionary<string, PublisherEntity>();
                    foreach (PublisherEntity publisher in publishersList)
                    {
                        publishers.Add(publisher.PublisherName, publisher);
                    }
                }

                return publishers;
            }
        }

        private static Dictionary<string, SubCategoryEntity> subCategories;
        public static Dictionary<string, SubCategoryEntity> SubCategories
        {
            get
            {
                if (subCategories == null)
                {
                    var subCategoriesList = new SubCategoryEntity[]
                    {
                        new SubCategoryEntity { SubCategoryName = "SubCategory Name 1", Category=categories["Category Name 1"] },
                    };

                    subCategories = new Dictionary<string, SubCategoryEntity>();

                    foreach (SubCategoryEntity subCategory in subCategoriesList)
                    {
                        subCategories.Add(subCategory.SubCategoryName, subCategory);
                    }
                }

                return subCategories;
            }
        }

        private static Dictionary<string, CategoryEntity> categories;
        public static Dictionary<string, CategoryEntity> Categories
        {
            get
            {
                if (categories == null)
                {
                    var categoriesList = new CategoryEntity[]
                    {
                        new CategoryEntity { CategoryName = "Category Name 1"},
                    };

                    categories = new Dictionary<string, CategoryEntity>();

                    foreach (CategoryEntity category in categoriesList)
                    {
                        categories.Add(category.CategoryName, category);
                    }
                }

                return categories;
            }
        }
    }
}
