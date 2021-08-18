namespace CarRentingSystem.Test.Mocks
{
    using System;
    using SecondHandClothes.Data;
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseMock
    {
        public static SecondHandDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<SecondHandDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new SecondHandDbContext(dbContextOptions);
            }
        }
    }
}