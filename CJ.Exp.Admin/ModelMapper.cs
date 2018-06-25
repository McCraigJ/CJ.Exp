using AutoMapper;
using CJ.Exp.Admin.Models.AccountViewModels;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.Admin.Models.ManageViewModels;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
  }
}
