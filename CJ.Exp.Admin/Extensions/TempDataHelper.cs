using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.Admin.Models.GridViewModels;
using CJ.Exp.BusinessLogic;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace CJ.Exp.Admin.Extensions
{
  public static class TempDataHelper
  {
    private static void Put<T>(ITempDataDictionary tempData, string key, T value) where T : class
    {
      tempData[key] = JsonConvert.SerializeObject(value);
    }

    private static T Get<T>(ITempDataDictionary tempData, string key) where T : class
    {
      object o;
      tempData.TryGetValue(key, out o);
      return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
    }

    public static void AddTempData<T>(this ITempDataDictionary tempData, string key, T data) where T : class
    {
      Put<T>(tempData, key, data);
    }

    public static T GetTempData<T>(this ITempDataDictionary tempData, string key, bool isRequired = true) where T : class
    {
      T data = Get<T>(tempData, key);

      if (isRequired && data == null)
      {
        throw new CjExpInvalidOperationException("Cannot find data");
      }

      return data;
    }

    public static T GetGridSearchFilter<T>(this ITempDataDictionary tempData, string filterKey, GridFilterViewModel filter = null) where T : SearchFilterBaseSM
    {
      var searchFilter = tempData.GetTempData<T>(filterKey, false);
      if (searchFilter == null)
      {
        return null;
      }

      if (searchFilter.GridFilter == null)
      {
        searchFilter.GridFilter = new GridRequestSM();
      }

      if (filter != null)
      {
        searchFilter.GridFilter.ItemsPerPage = filter.PageSize;
        var pageIndex = filter.PageIndex;
        searchFilter.GridFilter.PageNumber = pageIndex >= 0 ? pageIndex : 0;
      }

      tempData.AddTempData(filterKey, searchFilter);

      return searchFilter;
    }

    public static void AddGridSearchFilter<T>(this ITempDataDictionary tempData, string filterKey, T searchFilter) where T : SearchFilterBaseSM
    {
      tempData.AddTempData(filterKey, searchFilter);
    }
  }
}
