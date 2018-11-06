﻿using System.Collections.Generic;
using AutoMapper;
using CJ.Exp.Admin.Models.UsersViewModels;
using CJ.Exp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Roles;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize(Roles = "_admin")]
  public class UsersController : ControllerBase
  {
    private readonly IAuthService _authService;

    public UsersController(IAuthService authService)
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
      var addUserVM = new AddUserVM
      {
        Roles = CreateRolesList()
      };
      
      return View(addUserVM);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddUserVM addUser)
    {
      if (ModelState.IsValid)
      {
        var user = Mapper.Map<UserSM>(addUser);
        await _authService.AddUser(user, addUser.Password);
        if (_authService.BusinessErrors.Any())
        {
          MergeBusinessErrors(_authService.BusinessErrors);
          return View(addUser);
        }

        return RedirectToAction("Index");
      }

      addUser.Roles = CreateRolesList();
      return View(addUser);

    }

    public async Task<IActionResult> Edit(string id)
    {
      var user = _authService.GetUsers().SingleOrDefault(x => x.Id == id);
      if (user != null)
      {
        var userRole = await _authService.GetUserRole(user);
        var editUserVm = Mapper.Map<AddUserVM>(user);
        editUserVm.Roles = CreateRolesList();
        editUserVm.Role = userRole;
        return View(editUserVm);
      }

      return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AddUserVM updateUser)
    {
      if (ModelState.IsValid)
      {
        var user = Mapper.Map<UserSM>(updateUser);
        await _authService.UpdateUser(user);
        if (_authService.BusinessErrors.Any())
        {
          MergeBusinessErrors(_authService.BusinessErrors);
          return RedirectToAction("Index");
        }
      }
      
      updateUser.Roles = CreateRolesList();
      return View(updateUser);
    }
    
    private List<SelectListItem> CreateRolesList()
    {
      var rolesList = new List<SelectListItem>();      
      foreach (var role in ApplicationRoles.AllRoles())
      {
        rolesList.Add(new SelectListItem { Text = role.Value, Value = role.Key});
      }

      return rolesList;
    }

  }
}