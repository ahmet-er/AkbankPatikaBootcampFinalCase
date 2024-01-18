using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentCategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<PaymentCategoryResponse>>> Get()
        {
            var operation = new GetAllPaymentCategoryQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        public async Task<ApiResponse<PaymentCategoryResponse>> Get(int id)
        {
            var operation = new GetPaymentCategoryById(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("by-parameters")]
        public async Task<ApiResponse<List<PaymentCategoryResponse>>> GetByParameter(
            [FromQuery] string? Name)
        {
            var operation = new GetPaymentCategoryByParameter(Name);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<PaymentCategoryResponse>> Post([FromBody] PaymentCategoryRequest paymentRequest)
        {
            var operation = new CreatePaymentCategoryCommand(paymentRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("id")]
        public async Task<ApiResponse> Put(int id, [FromBody] PaymentCategoryRequest paymentRequest)
        {
            var operation = new UpdatePaymentCategoryCommand(id, paymentRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeletePaymentCategoryCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
