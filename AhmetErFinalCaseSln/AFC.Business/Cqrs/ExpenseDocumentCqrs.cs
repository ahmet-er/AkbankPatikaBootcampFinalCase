using AFC.Base.Response;
using AFC.Schema;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AFC.Business.Cqrs;

//public record CreateExpenseDocumentCommand(ExpenseDocumentRequest Model) : IRequest<ApiResponse<ExpenseDocumentResponse>>;
public record CreateExpenseDocumentCommand(int ExpenseRequestId, IFormFile FormFile) : IRequest<ApiResponse<ExpenseDocumentResponse>>;
public record UpdateExpenseDocumentCommand(int Id, ExpenseDocumentRequest Model) : IRequest<ApiResponse<ExpenseDocumentResponse>>;
public record DeleteExpenseDocumentCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenseDocumentQuery() : IRequest<ApiResponse<List<ExpenseDocumentResponse>>>;
public record GetExpenseDocumentByIdQuery(int Id) : IRequest<ApiResponse<ExpenseDocumentResponse>>;
public record GetExpenseDocumentByParameterQuery(int? ExpenseRequestId, string FileType, string FileName) : IRequest<ApiResponse<List<ExpenseDocumentResponse>>>;