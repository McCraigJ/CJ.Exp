using AutoMapper;
using CJ.Exp.Admin.Models.AccountViewModels;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.Admin.Models.ManageViewModels;
using CJ.Exp.Admin.Models.UsersViewModels;
using CJ.Exp.ServiceModels.Expenses;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.Admin
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {
      CreateMap<RegisterViewModel, UserSM>();
      CreateMap<UserSM, RegisterViewModel>();

      CreateMap<UserSM, IndexViewModel>();
      CreateMap<IndexViewModel, UserSM>();

      CreateMap<ExpenseTypeVM, ExpenseTypeSM>();
      CreateMap<ExpenseTypeSM, ExpenseTypeVM>();

      CreateMap<ExpenseVM, ExpenseSM>()
        //.ForMember(dst => dst.SyncDate, opt => opt.Ignore())
        .ForMember(dst => dst.ExpenseType, opt => opt.Ignore());
        //.ForMember(dst => dst.ExpenseType, opt => opt.Ignore())



      CreateMap<ExpenseSM, ExpenseVM>()
        .ForMember(dst => dst.ExpenseTypeId, opt => opt.MapFrom(src => src.ExpenseType.Id))
        .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

      CreateMap<UserSM, UserVM>();

      CreateMap<UserSM, AddUserVM>();
      CreateMap<UserSM, EditUserVM>();

    }
  }
}
