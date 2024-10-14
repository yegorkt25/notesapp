using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class NotesAppContext : DbContext
	{
		public NotesAppContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Note> Notes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("users");
			modelBuilder.Entity<Note>().ToTable("notes");
			modelBuilder.Entity<User>().HasMany(e => e.Notes).WithOne(e => e.User).HasForeignKey(e => e.UserId).HasPrincipalKey(e => e.Id);
		}
	}
}
