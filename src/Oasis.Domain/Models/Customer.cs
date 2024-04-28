using System.ComponentModel.DataAnnotations;

namespace Oasis.Domain.Models
{
	public class Customer
	{
		public int CustomerId { get; set; }

		public required string Name { get; set; }

		public required string Email { get; set; }

		public required string Password { get; set; }

		public ICollection<BankAccount>? BankAccounts { get; set; }
	}
}
