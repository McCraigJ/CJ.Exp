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

      CreateMap<ExpenseTypeVM, UpdateExpenseTypeSM>();
      CreateMap<ExpenseTypeVM, ExpenseTypeSM>();
      CreateMap<UpdateExpenseTypeSM, ExpenseTypeVM>();
      CreateMap<ExpenseTypeSM, ExpenseTypeVM>();

      CreateMap<ExpenseVM, ExpenseSM>();

      CreateMap<ExpenseVM, UpdateExpenseSM>()
        //.ForMember(dst => dst.SyncDate, opt => opt.Ignore())
        .ForMember(dst => dst.ExpenseType, opt => opt.Ignore());
        //.ForMember(dst => dst.ExpenseType, opt => opt.Ignore())



      CreateMap<UpdateExpenseSM, ExpenseVM>()
        .ForMember(dst => dst.ExpenseTypeId, opt => opt.MapFrom(src => src.ExpenseType.Id))
        .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

      CreateMap<UserSM, UserVM>();

      CreateMap<UserSM, AddUserVM>();
      CreateMap<UserSM, EditUserVM>();

      CreateMap<ExpensesFilterSM, ExpensesFilterVM>()
        .ForMember(x => x.IsFiltered, opt => opt.Ignore());
      //.ForMember(x => x);

      CreateMap<ExpensesFilterVM, ExpensesFilterSM>()
        .ForMember(x => x.GridFilter, opt => opt.Ignore());

      CreateMap<ExpenseSM, ExpenseVM>();

      //CreateMap<ExpensesFilterVM, ExpensesFilterSM>()
      //  .ForMember(x => x, opt => opt.Ignore());

    }
  }
}
