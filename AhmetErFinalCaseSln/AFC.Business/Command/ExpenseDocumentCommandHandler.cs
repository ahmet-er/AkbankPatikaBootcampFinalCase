using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Business.Service;
using AFC.Data;
using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;
using MediatR;
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

    public ExpenseDocumentCommandHandler(AfcDbContext dbContext, IMapper mapper, IAzureBlobStorageService azureBlobStorageService)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.azureBlobStorageService = azureBlobStorageService;
    }

    public async Task<ApiResponse<ExpenseDocumentResponse>> Handle(CreateExpenseDocumentCommand request, CancellationToken cancellationToken)
    {
        var uploadedFileResponse = await azureBlobStorageService.UploadFileAsync(request.Model.FormFile);

        var expenseDocument = new ExpenseDocument
        {
            ExpenseRequestId = request.Model.ExpenseRequestId,
            FileName = uploadedFileResponse.FileName,
            FileType = uploadedFileResponse.FileType,
            FilePath = uploadedFileResponse.FilePath,
        };

        var entityResult = await dbContext.AddAsync(expenseDocument, cancellationToken);
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
