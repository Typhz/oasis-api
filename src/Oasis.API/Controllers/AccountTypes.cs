using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Oasis.Enums;

namespace Oasis.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountTypes : ControllerBase
{
	[HttpGet]
	public ActionResult<IEnumerable<EnumDisplay>> GetAccountTypes()
	{
		var accountTypes = new List<EnumDisplay>();

		foreach (AccountType accountType in Enum.GetValues(typeof(AccountType)))
		{
			var displayAttribute = accountType.GetType()
				.GetField(accountType.ToString())
				.GetCustomAttributes(typeof(DisplayAttribute), false);

			var displayName = displayAttribute.Length > 0
				? ((DisplayAttribute)displayAttribute[0]).Name
				: accountType.ToString();

			accountTypes.Add(new EnumDisplay
			{
				Code = accountType.ToString(),
				Name = displayName
			});
		}

		return Ok(accountTypes);
	}

	public class EnumDisplay
	{
		public string Code { get; set; }
		public string Name { get; set; }
	}
}
