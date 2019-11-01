using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CJ.Exp.Admin.Extensions;
using CJ.Exp.Admin.Models.GridViewModels;
using CJ.Exp.Admin.Models.UsersViewModels;
using CJ.Exp.BusinessLogic.Users;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize(Roles = "Admin")]
  public class UsersController : ControllerBase
  {
    private const string FilterDataKey = "UsersFilter";

    private readonly IAuthService _authService;

    public UsersController(ILoggerFactory loggerFactory, IAuthService authService, ILanguage language) : 
      base (loggerFactory.CreateLogger<UsersController>(), language)
    {
      _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return IndexInternal();
    }

    [HttpPost]
    public IActionResult BackToIndex()
    {
      return IndexInternal();
    }

    private IActionResult IndexInternal()
    {
      var vm = CreateUsersGridFilterFromTempData();
      return View("Index", vm);
    }

    private UsersVM CreateUsersGridFilterFromTempData()
    {
      var model = new UsersVM();

      var filter = TempData.GetGridSearchFilter<UsersFilterSM>(FilterDataKey) ?? new UsersFilterSM();

      if (filter?.GridFilter != null)
      {
        SetPageOption(model, "CurrentPage", filter.GridFilter.PageNumber.ToString());
      }

      TempData.AddGridSearchFilter(FilterDataKey, filter);

      return model;
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public IActionResult GetUsersData(GridFilterViewModel filter)
    {
      var searchFilter = TempData.GetGridSearchFilter<UsersFilterSM>(FilterDataKey, filter);

      return new JsonResult(_authService.GetUsers(searchFilter));
    }

    [HttpPost]
    public IActionResult Add()
    {      
      return GetAddView(new AddUserVM());
    }

    [HttpPost]
    public async Task<IActionResult> DoAdd(AddUserVM addUser)
    {
      if (ModelState.IsValid)
      {
        var user = Mapper.Map<UserSM>(addUser);
        await _authService.AddUser(user, addUser.Password);
        await _authService.UpdateRole(user.Email, addUser.Role);
        if (_authService.BusinessErrors.Any())
        {
          MergeBusinessErrors(_authService.BusinessErrors);
          return GetAddView(addUser);
        }
        SetControllerMessage(ControllerMessageType.Success, "Added");
        return RedirectToAction("Index");
      }
      
      return GetAddView(addUser);

    }

    private IActionResult GetAddView(AddUserVM model)
    {
      model.Roles = CreateRolesList();
      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string editValue)
    {
      var user = _authService.GetUserById(editValue);
      if (user != null)
      {
        var userRole = await _authService.GetUserRole(user);
        var editUserVm = Mapper.Map<EditUserVM>(user);
        editUserVm.Roles = CreateRolesList();
        editUserVm.Role = userRole;
        return View(editUserVm);
      }

      return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DoEdit(EditUserVM updateUser)
    {
      if (ModelState.IsValid)
      {
        var user = Mapper.Map<UserSM>(updateUser);
        await _authService.UpdateUser(user);
        await _authService.UpdateRole(user.Email, updateUser.Role);
        if (_authService.BusinessErrors.Any())
        {
          MergeBusinessErrors(_authService.BusinessErrors);
        } else {
          SetControllerMessage(ControllerMessageType.Success, "Updated");
          return RedirectToAction("Index");
        }
      }
      
      updateUser.Roles = CreateRolesList();
      return View("Edit", updateUser);
    }

    [HttpPost]
    public IActionResult Delete(string editValue)
    {
      var user = _authService.GetUserById(editValue);
      if (user == null)
      {
        SetControllerMessage(ControllerMessageType.Error, "UserNotFound");
        return RedirectToAction("Index");
      }

      return View("Delete", Mapper.Map<UserVM>(user));
    }

    [HttpPost]
    public async Task<IActionResult> DoDelete(UserVM model)
    {
      if (ModelState.IsValid)
      {
        var user = Mapper.Map<UserSM>(model);
        await _authService.DeleteUser(user);
        if (_authService.BusinessErrors.Any())
        {
          MergeBusinessErrors(_authService.BusinessErrors);
        }
        else
        {
          SetControllerMessage(ControllerMessageType.Success, "Deleted");
          return RedirectToAction("Index");
        }
        
      }

      return View("Delete", model);
    }

    private List<SelectListItem> CreateRolesList()
    {
      var rolesList = new List<SelectListItem>();      
      foreach (var role in ApplicationRolesDefinition.AllRoles())
      {
        rolesList.Add(new SelectListItem { Text = role, Value = role });
      }

      return rolesList;
    }

  }
}