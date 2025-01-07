using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Projet_5.Models;
using Projet_5.Services;

namespace Projet_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{vin}")]
        public async Task<IActionResult> GetTransactionByVin(int id)
        {
            var transaction= await _transactionService.GetTransactionsByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(transaction);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransaction()
        {
            var transactions = await _transactionService.GetAllTransactionAsync();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdTransaction = await _transactionService.AddTransactionAsync(transaction.Amount, transaction.VehicleId);

            return CreatedAtAction(nameof(GetTransactionByVin), new { id = createdTransaction.Id }, createdTransaction);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction updatedTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedTransaction.Id)
            {
                return BadRequest("L'ID de la transaction ne correspond pas.");
            }

            try
            {
               
                var success = await _transactionService.UpdateTransactionAsync(updatedTransaction, id);

                if (!success)
                {
                    return NotFound("Transaction introuvable.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }


    }
}
