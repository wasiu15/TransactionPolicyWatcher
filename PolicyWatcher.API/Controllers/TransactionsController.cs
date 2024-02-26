using Microsoft.AspNetCore.Mvc;
using PolicyWatcher.Domain.Dtos.Request;
using PolicyWatcher.Domain.Dtos.Response;
using PolicyWatcher.Domain.Interfaces.Service;
using PolicyWatcher.Domain.Models;

namespace PolicyWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TransactionsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("ActivateWatcher")]
        public async Task<IActionResult> ActivateWatcher(int minute)
        {
            await _serviceManager.transactionService.StartTransactionWatcher(minute);
            
            var response = new GenericResponse<string> { ResponseCode = "00", IsSuccessful = true, ResponseMessage = "Policy watcher is not running...", Data = null };
            return Ok(response);
        }

        [HttpGet("GetTransactions")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<Transaction>))]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _serviceManager.transactionService.GetTransactions();
            return Ok(transactions);
        }

        [HttpPost("CreateTransaction")]
        [ProducesResponseType(200, Type = typeof(GenericResponse<Transaction>))]
        public async Task<IActionResult> CreateTransaction(TransactionRequestDto transactionRequestDto)
        {
            var transaction = await _serviceManager.transactionService.CreateTransaction(transactionRequestDto);
            return Ok(transaction);
        }
    }
}