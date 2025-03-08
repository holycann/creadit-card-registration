using CC_Regist_System.Models;
using CC_Regist_System.Models.Address;
using CC_Regist_System.Models.Cards;
using CC_Regist_System.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace CC_Regist_System.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options) { }

        public DbSet<CountryModel> Country { get; set; }
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<CityModel> City { get; set; }
        public DbSet<ProvinceModel> Province { get; set; }
        public DbSet<AddressModel> Address { get; set; }
        public DbSet<UserDetailsModel> UserDetails { get; set; }
        public DbSet<CardsModel> Cards { get; set; }
        public DbSet<CardDetailsModel> CardDetails { get; set; }
        public DbSet<FilesModel> Files { get; set; }
    }
}
