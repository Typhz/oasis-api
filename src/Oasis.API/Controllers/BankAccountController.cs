using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.API.DTOs;
using Oasis.Domain.Models;
using Oasis.Infra.Context;

namespace Oasis.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BankAccountController(EntityContext context) : ControllerBase
{

	[HttpGet]
	public ActionResult Get()
	{
		var customer = HttpContext.Items["Customer"] as Customer;
		if (customer == null)
		{
			return NotFound("Cliente não encontrado.");
		}

		var bankAccounts = context.BankAccounts
			.Include(ba => ba.Bank)
			.Where(ba => ba.CustomerId == customer.CustomerId)
			.Select(ba => new
			{
				AccountName = ba.AccountName,
				BankName = ba.Bank.Name,
				OtherBankName = ba.OtherBankName
			})
			.ToList();

		return Ok(bankAccounts);
	}

	[HttpPost]
	public ActionResult Post([FromBody] BankAccountDto bankAccountDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var customer = HttpContext.Items["Customer"] as Customer;
		if (customer == null)
		{
			return NotFound("Cliente não encontrado.");
		}

		var bankAccount = new BankAccount
		{
			AccountName = bankAccountDto.AccountName,
			OtherBankName = bankAccountDto.OtherBankName,
			BankId = bankAccountDto.BankId,
			CustomerId = customer.CustomerId,
		};

		try
		{
			context.BankAccounts.Add(bankAccount);
			context.SaveChanges();
			return Ok("Conta bancária adicionada com sucesso.");
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
}
