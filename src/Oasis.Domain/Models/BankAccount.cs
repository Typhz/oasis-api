using System.ComponentModel.DataAnnotations;
using Oasis.Enums;

namespace Oasis.Domain.Models
{
	public class Bank
	{
		public int BankId { get; set; }
		public string Name { get; set; }
	}

	public class BankAccount
	{
		[Key]
		public int BankAccountId { get; set; }

		[Required]
		public string AccountName { get; set; }

		[Required]
		public int BankId { get; set; }
		public Bank? Bank { get; set; }

		public string? OtherBankName { get; set; }

		[Required]
		public AccountType AccountType { get; set; }

		[Required]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; } = null!;

		public List<Transaction> Transactions { get; set; }
	}
}
