using System.ComponentModel.DataAnnotations;
using Oasis.Domain.Models;

namespace Oasis.API.DTOs;

public class BankAccountDto
{
	[Key]
	public int BankAccountId { get; set; }

	[Required]
	public string AccountName { get; set; }

	[Required]
	public int BankId { get; set; }
	public Bank? Bank { get; set; }

	public string? OtherBankName { get; set; }

}
