using Microsoft.AspNetCore.Identity;
using Oasis.Domain.Models;
using Oasis.Infra.Context;

namespace Oasis.API.Seed;

public class DbInitializer
{
	public static void Initialize(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<EntityContext>();

		context.Database.EnsureCreated();

		if (!context.Banks.Any())
		{
			SeedBanks(context);
		}

		if (!context.Customers.Any())
		{
			SeedCustomers(context);
		}
	}

	private static void SeedBanks(EntityContext context)
	{
		var banks = new[]
		{
			new Bank { Name = "Bradesco" },
			new Bank { Name = "Itau" },
			new Bank { Name = "Caixa" },
			new Bank { Name = "Banco do Brasil" },
			new Bank { Name = "Santander" }
		};

		context.Banks.AddRange(banks);
		context.SaveChanges();
	}

	private static void SeedCustomers(EntityContext context)
	{
		var hasher = new PasswordHasher<Customer>();


		var customers = new[]
		{
			new Customer { Name = "Patrick", Email = "pk@gmail.com", Password = hasher.HashPassword(null, "2004") },
		};

		context.Customers.AddRange(customers);
		context.SaveChanges();
	}
}
