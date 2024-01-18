using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Business.Helpers;
using AFC.Business.Service;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AFC.Business.Command;

public class ExpenseDocumentCommandHandler :
    IRequestHandler<CreateExpenseDocumentCommand, ApiResponse<ExpenseDocumentResponse>>,
    IRequestHandler<UpdateExpenseDocumentCommand, ApiResponse<ExpenseDocumentResponse>>,
    IRequestHandler<DeleteExpenseDocumentCommand, ApiResponse>
{
    private readonly AfcDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IAzureBlobStorageService azureBlobStorageService;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ExpenseDocumentCommandHandler(AfcDbContext dbContext, IMapper mapper, IAzureBlobStorageService azureBlobStorageService, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.azureBlobStorageService = azureBlobStorageService;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<ExpenseDocumentResponse>> Handle(CreateExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        var uploadedFileResponse = await azureBlobStorageService.UploadFileAsync(request.FormFile);

        var expenseDocument = new ExpenseDocument
        {
            ExpenseRequestId = request.ExpenseRequestId,
            FileName = uploadedFileResponse.FileName,
            FileType = uploadedFileResponse.FileType,
            FilePath = uploadedFileResponse.FilePath,
        };

        BaseEntitySetPropertyExtension.SetCreatedProperties(expenseDocument, httpContextAccessor);

        var entityResult = await dbContext.AddAsync(expenseDocument, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenseDocument, ExpenseDocumentResponse>(entityResult.Entity);
        return new ApiResponse<ExpenseDocumentResponse>(mapped);
    }

    public async Task<ApiResponse<ExpenseDocumentResponse>> Handle(UpdateExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseDocument>()
            .FirstOrDefaultAsync(x => x.Id ==  request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse<ExpenseDocumentResponse>("Record not found.");

        var oldFilePath = fromdb.FilePath;

        var uploadedFileResponse = await azureBlobStorageService.UploadFileAsync(request.Model.FormFile);

        fromdb.FileName = uploadedFileResponse.FileName;
        fromdb.FileType = uploadedFileResponse.FileType;
        fromdb.FilePath = uploadedFileResponse.FilePath;

        BaseEntitySetPropertyExtension.SetModifiedProperties(fromdb, httpContextAccessor);

        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenseDocument, ExpenseDocumentResponse>(fromdb);
        return new ApiResponse<ExpenseDocumentResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(DeleteExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseDocument>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (fromdb is null)
            return new ApiResponse("Record not found.");

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
