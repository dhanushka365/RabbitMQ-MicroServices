using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ_MicroServices.Banking.Application.Interfaces;
using RabbitMQ_MicroServices.Banking.Domain.Models;

namespace RabbitMQ_MicroServices.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public BankingController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get() 
        {
            return Ok(_accountService.GetAccounts());
        }


    }
}
