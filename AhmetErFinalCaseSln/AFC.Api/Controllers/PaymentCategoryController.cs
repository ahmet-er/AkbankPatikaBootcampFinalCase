using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFC.Api.Controllers
{
    [Route("api/payment-category")]
    [ApiController]
    public class PaymentCategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentCategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<List<PaymentCategoryResponse>>> Get()
        {
            var operation = new GetAllPaymentCategoryQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("id")]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<PaymentCategoryResponse>> Get(int id)
        {
            var operation = new GetPaymentCategoryByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("by-parameters")]
        [Authorize(Roles = "Admin, FieldStaff")]
        public async Task<ApiResponse<List<PaymentCategoryResponse>>> GetByParameter(
            [FromQuery] string? Name)
        {
            var operation = new GetPaymentCategoryByParameterQuery(Name);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<PaymentCategoryResponse>> Post([FromBody] PaymentCategoryRequest paymentRequest)
        {
            var operation = new CreatePaymentCategoryCommand(paymentRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Put(int id, [FromBody] PaymentCategoryRequest paymentRequest)
        {
            var operation = new UpdatePaymentCategoryCommand(id, paymentRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeletePaymentCategoryCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
