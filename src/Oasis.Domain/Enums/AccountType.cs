using System.ComponentModel.DataAnnotations;

namespace Oasis.Enums;

public enum AccountType
{
	[Display(Name = "Conta Corrente")]
	Checking,
	[Display(Name = "Conta Poupança")]
	Savings,
}
