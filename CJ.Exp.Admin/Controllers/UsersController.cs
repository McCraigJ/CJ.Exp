using AutoMapper;
using CJ.Exp.Admin.Models.UsersViewModels;
using CJ.Exp.BusinessLogic.Users;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize(Roles = "Admin")]
  public class UsersController : ControllerBase
  {
    private readonly IAuthService _authService;

    public UsersController(ILoggerFactory loggerFactory, IAuthService authService, ILanguage language) : 
      base (loggerFactory.CreateLogger<UsersController>(), language)
    {
      _authService = authService;
    }
    public IActionResult Index()
    {
      var users = _authService.GetUsers().ToList();

      var vm = new UsersVM
      {
        Users = Mapper.Map<List<UserVM>>(users)
      };
      return View(vm);
    }

    public IActionResult Add()
    {      
      return GetAddView(new AddUserVM());
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddUserVM addUser)
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

        return RedirectToAction("Index");
      }
      
      return GetAddView(addUser);

    }

    private IActionResult GetAddView(AddUserVM model)
    {
      model.Roles = CreateRolesList();
      return View(model);
    }

    public async Task<IActionResult> Edit(string id)
    {
      var user = _authService.GetUserById(id);
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
    public async Task<IActionResult> Edit(EditUserVM updateUser)
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
          return RedirectToAction("Index");
        }
      }
      
      updateUser.Roles = CreateRolesList();
      return View(updateUser);
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