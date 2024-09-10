using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PapaSmurfie.Models;
using Microsoft.AspNetCore.Identity;
using PapaSmurfie.Models.Models;


namespace PapaSmurfie.Database
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<GameModel> Games { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OwnedGameModel> OwnedGames { get; set; }
        public DbSet<FriendsListModel> FriendsList { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }
    }
}
