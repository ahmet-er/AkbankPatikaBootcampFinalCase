using AFC.Base.Enums;
using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Business.Helpers;
using AFC.Business.Service;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AFC.Business.Command;

public class ExpensePaymentCommandHandler :
    IRequestHandler<CreateExpensePaymentCommand, ApiResponse<ExpensePaymentResponse>>
{
    const string CompanyIBAN = "TR330006100519786457841326";
    const string IBANOwnerName = "Akbank";
    const string TransferType = "EFT";
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly INotificationService notificationService;

    public ExpensePaymentCommandHandler(AfcDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, INotificationService notificationService)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
        this.notificationService = notificationService;
    }

    public async Task<ApiResponse<ExpensePaymentResponse>> Handle(CreateExpensePaymentCommand request, CancellationToken cancellationToken)
    {
        var expenseRequest = await dbContext.Set<ExpenseRequest>()
            .Include(x => x.FieldStaff)
            .Include(x => x.FieldStaff.User)
            .Where(x => x.Id == request.Model.ExpenseRequestId && x.IsActive)
            .FirstOrDefaultAsync(cancellationToken);

        expenseRequest.PaymentStatus = PaymentStatus.Paid;

        BaseEntitySetPropertyExtension.SetModifiedProperties(expenseRequest, httpContextAccessor);

        await dbContext.SaveChangesAsync(cancellationToken);

        var mappedExpenseRequest = mapper.Map<ExpenseRequest, ExpenseRequestResponse>(expenseRequest); 

        var response = new ExpensePaymentResponse()
        {
            ExpenseRequestId = request.Model.ExpenseRequestId,
            ExpenseRequest = mappedExpenseRequest,
            TransferType = TransferType,
            PaymentDescription = $"Payment from {IBANOwnerName} {CompanyIBAN} to {expenseRequest.FieldStaff.User.FirstName} {expenseRequest.FieldStaff.User.LastName} {expenseRequest.FieldStaff.IBAN} - EFT transaction amounting to {expenseRequest.Amount} {request.Model.CurrencyType} was completed successfully."
        };

        string subject = $"{DateTime.Now} EFT Notification";
        BackgroundJob.Schedule(() => notificationService.SendEmail(subject, expenseRequest.FieldStaff.User.Email, response.PaymentDescription), TimeSpan.FromMinutes(1));
        

        return new ApiResponse<ExpensePaymentResponse>(response);
    }
}
