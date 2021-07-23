namespace SecondHandClothes.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using System.Linq;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<SecondHandDbContext>();

            data.Database.Migrate();

            SeedCategories(data);
            SeedConditions(data);
            SeedSexes(data);
            SeedSizes(data);


            return app;
        }

        private static void SeedCategories(SecondHandDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {CategoryName = "Блузи"},
                new Category {CategoryName = "Бельо"},
                new Category {CategoryName = "Дънки"},
                new Category {CategoryName = "Тениски"},
                new Category {CategoryName = "Панталони"},
                new Category {CategoryName = "Анцузи"},
                new Category {CategoryName = "Ризи"},
                new Category {CategoryName = "Сака"},
                new Category {CategoryName = "Якета"},
                new Category {CategoryName = "Чанти"},
                new Category {CategoryName = "Елеци"},
                new Category {CategoryName = "Жилетки"},
                new Category {CategoryName = "Други"}

            });

            data.SaveChanges();
        }

        private static void SeedConditions(SecondHandDbContext data)
        {
            if (data.Conditions.Any())
            {
                return;
            }

            data.Conditions.AddRange(new[]
            {
                new Condition {ConditionType = "Лошо"},
                new Condition {ConditionType = "Средно"},
                new Condition {ConditionType = "Добро"},
                new Condition {ConditionType = "Отлично"}
            });

            data.SaveChanges();
        }

        private static void SeedSexes(SecondHandDbContext data)
        {
            if (data.Sexes.Any())
            {
                return;
            }

            data.Sexes.AddRange(new[]
            {
                new Sex {SexType = "Мъж"},
                new Sex {SexType = "Жена"},
                new Sex {SexType = "Унисекс"},
                new Sex {SexType = "Друг"}
            });

            data.SaveChanges();
        }
        private static void SeedSizes(SecondHandDbContext data)
        {
            if (data.Sizes.Any())
            {
                return;
            }

            data.Sizes.AddRange(new[]
            {
                new Size {SizeType = "2XS"},
                new Size {SizeType = "XS"},
                new Size {SizeType = "S"},
                new Size {SizeType = "M"},
                new Size {SizeType = "L"},
                new Size {SizeType = "XL"},
                new Size {SizeType = "2XL"},
                new Size {SizeType = "3XL"}
            });

            data.SaveChanges();
        }
    }
}
