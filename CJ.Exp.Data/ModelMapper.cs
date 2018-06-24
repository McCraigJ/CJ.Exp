using AutoMapper;
using CJ.Exp.Data.Models;
using CJ.Exp.ServiceModels.Expenses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.Data
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {
      CreateMap<ExpenseTypeDM, ExpenseTypeSM>();
      CreateMap<ExpenseTypeSM, ExpenseTypeDM>();

      CreateMap<ExpenseDM, ExpenseSM>();
      CreateMap<ExpenseSM, ExpenseDM>();

    }
  }
}
