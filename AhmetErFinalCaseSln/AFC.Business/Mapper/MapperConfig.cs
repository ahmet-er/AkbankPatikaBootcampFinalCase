using AFC.Data.Entity;
using AFC.Schema;
using AutoMapper;

namespace AFC.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<ExpenseRequestRequest, ExpenseRequest>();
        CreateMap<ExpenseRequest, ExpenseRequestResponse>();

        CreateMap<ExpenseDocumentRequest, ExpenseDocument>();
        CreateMap<ExpenseDocument, ExpenseDocumentResponse>();

        CreateMap<FieldStaffRequest, FieldStaff>();
        CreateMap<FieldStaff, FieldStaffResponse>();

        CreateMap<PaymentCategoryRequest, PaymentCategory>();
        CreateMap<PaymentCategory, PaymentCategoryResponse>();

        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}
