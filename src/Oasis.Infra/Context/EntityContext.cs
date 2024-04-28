using Microsoft.EntityFrameworkCore;
using Oasis.Domain.Models;

namespace Oasis.Infra.Context;

public class EntityContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Customer> Customers { get; set; }
	public DbSet<BankAccount> BankAccounts { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
	public DbSet<Bank> Banks { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer>().ToTable("customers");
		modelBuilder.Entity<Customer>().Property(c => c.CustomerId).HasColumnName("id");
		modelBuilder.Entity<Customer>().Property(c => c.Name).HasColumnName("name");
		modelBuilder.Entity<Customer>().Property(c => c.Email).HasColumnName("email");
		modelBuilder.Entity<Customer>().Property(c => c.Password).HasColumnName("password");

		modelBuilder.Entity<Customer>(entity =>
		{
			entity.HasKey(e => e.CustomerId);
			entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();

			entity.HasMany(c => c.BankAccounts)
					.WithOne(ba => ba.Customer)
					.HasForeignKey(ba => ba.CustomerId);
		});

		modelBuilder.Entity<Bank>().ToTable("banks");
		modelBuilder.Entity<Bank>().Property(b => b.BankId).HasColumnName("id");
		modelBuilder.Entity<Bank>().Property(b => b.Name).HasColumnName("name");

		modelBuilder.Entity<Bank>(entity =>
		{
			entity.HasKey(e => e.BankId);
			entity.Property(e => e.BankId).ValueGeneratedOnAdd();
		});


		modelBuilder.Entity<Transaction>().ToTable("transactions");
		modelBuilder.Entity<Transaction>().Property(t => t.TransactionId).HasColumnName("id");
		modelBuilder.Entity<Transaction>().Property(t => t.Date).HasColumnName("date");
		modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasColumnName("amount");
		modelBuilder.Entity<Transaction>().Property(t => t.Type).HasColumnName("type");
		modelBuilder.Entity<Transaction>().Property(t => t.Description).HasColumnName("description");
		modelBuilder.Entity<Transaction>().Property(t => t.BankAccountId).HasColumnName("bank_account_id");

		modelBuilder.Entity<BankAccount>().ToTable("bank_accounts");
		modelBuilder.Entity<BankAccount>().Property(ba => ba.BankAccountId).HasColumnName("id");
		modelBuilder.Entity<BankAccount>().Property(ba => ba.AccountName).HasColumnName("account_name");
		modelBuilder.Entity<BankAccount>().Property(ba => ba.BankId).HasColumnName("bank_id");
		modelBuilder.Entity<BankAccount>().Property(ba => ba.OtherBankName).HasColumnName("other_bank_name");
		modelBuilder.Entity<BankAccount>().Property(ba => ba.AccountType).HasColumnName("account_type");
		modelBuilder.Entity<BankAccount>().Property(ba => ba.CustomerId).HasColumnName("customer_id");

		modelBuilder.Entity<BankAccount>().HasOne(ba => ba.Customer)
			.WithMany(c => c.BankAccounts)
			.HasForeignKey(ba => ba.CustomerId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<BankAccount>(entity =>
		{
			entity.HasOne(ba => ba.Bank)
				.WithMany()
				.HasForeignKey(ba => ba.BankId)
				.OnDelete(DeleteBehavior.Restrict);
		});
	}
}
