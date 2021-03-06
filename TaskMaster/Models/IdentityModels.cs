﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskMaster.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       public DbSet<Projetos> Projetos { get; set; }
       public DbSet<GerenteProjs> GerenteProjs { get; set; }
       public DbSet<Tasks> Tasks { get; set; }
       public DbSet<Testers> Testers { get; set; }
       public DbSet<Bugs> Bugs { get; set; }
       public DbSet<Devs> Devs { get; set; }
       public DbSet<TiposBugs> TiposBugs { get; set; }
       public DbSet<TiposTestes> TiposTestes { get; set; }
       public DbSet<EstadosBug> EstadosBugs { get; set; }
       public DbSet<NotasTrabalhoBug> NotasTrabalhoBug { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}