using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Tech Innovations",
                    Address = "123 Tech Road",
                    City = "Techville",
                    State = "CA",
                    PostalCode = "90001",
                    PhoneNumber = "555-1234"
                },
                new Company
                {
                    Id = 2,
                    Name = "Green Solutions",
                    Address = "456 Green Ave",
                    City = "EcoCity",
                    State = "NY",
                    PostalCode = "10001",
                    PhoneNumber = "555-5678"
                },
                new Company
                {
                    Id = 3,
                    Name = "Digital Horizons",
                    Address = "789 Digital Blvd",
                    City = "CyberTown",
                    State = "TX",
                    PostalCode = "75001",
                    PhoneNumber = "555-8765"
                },
                new Company
                {
                    Id = 4,
                    Name = "Global Ventures",
                    Address = "101 World St",
                    City = "GlobeCity",
                    State = "FL",
                    PostalCode = "33101",
                    PhoneNumber = "555-4321"
                },
                new Company
                {
                    Id = 5,
                    Name = "Bright Ideas",
                    Address = "202 Innovation Way",
                    City = "IdeaCity",
                    State = "WA",
                    PostalCode = "98001",
                    PhoneNumber = "555-9876"
                }
            );

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "3fd4167c-bab3-4064-8940-5854c6d1723f",
                    UserName = "666",
                    Email = "admin@bulkybook.com",
                    Password = "666",
                    Name = "Admin",
                    CompanyId = 1,
                    Role = StaticDetails.Role_Admin
                }
             );

            modelBuilder.Entity<CoverType>().HasData(
                new CoverType { Id = 1, Name = "Hardcover" },
                new CoverType { Id = 2, Name = "Paperback" },
                new CoverType { Id = 3, Name = "Leatherbound" },
                new CoverType { Id = 4, Name = "Spiral-bound" },
                new CoverType { Id = 5, Name = "Dust Jacket" },
                new CoverType { Id = 6, Name = "Flexibound" },
                new CoverType { Id = 7, Name = "Casebound" },
                new CoverType { Id = 8, Name = "Board Book" },
                new CoverType { Id = 9, Name = "Cloth-bound" },
                new CoverType { Id = 10, Name = "Matte Finish" },
                new CoverType { Id = 11, Name = "Glossy Finish" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Romance", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Comedy", DisplayOrder = 5 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SWD9999001",
                    Price = 90,
                    CategoryId = 3,
                    ImageUrl = "d1cc2fbb-658b-4d51-8892-80b4158be721.jpg",
                    CoverTypeId = 1,
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    Price = 30,
                    CategoryId = 1,
                    ImageUrl = "42ba0ff9-e11d-46ee-940b-4b15b97d0436.jpg",
                    CoverTypeId = 4,
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    Price = 50,
                    CategoryId = 1,
                    ImageUrl = "7e929519-0e12-42d7-be48-d8b0cc3c2f2c.jpg",
                    CoverTypeId = 7,
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    Price = 65,
                    CategoryId = 2,
                    ImageUrl = "176acaf1-a77d-41e3-a4bf-00b6c591ad29.jpg",
                    CoverTypeId = 11,
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    Price = 27,
                    CategoryId = 2,
                    ImageUrl = "1c15ca0b-ac94-4ad3-b21d-d01bc6fdb40f.jpg",
                    CoverTypeId = 6,
                },
                new Product
                {
                    Id = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    Price = 23,
                    CategoryId = 3,
                    ImageUrl = "1c09e2e4-ba21-443c-8b7b-ad20f2547063.jpg",
                    CoverTypeId = 2,
                }
            );
        }
    }
}
