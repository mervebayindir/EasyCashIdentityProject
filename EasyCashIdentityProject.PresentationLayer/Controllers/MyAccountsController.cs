﻿using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    [Authorize]
    public class MyAccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public MyAccountsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserEditDto appUserEditDto = new AppUserEditDto()
            {
                Name = values.Name,
                SurName = values.SurName,
                PhoneNumber = values.PhoneNumber,
                Email = values.Email,
                City = values.City,
                District = values.District,
                ImageUrl = values.ImageUrl,
            };
            return View(appUserEditDto);
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserEditDto appUserEditDto)
        {
            if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.Name = appUserEditDto.Name;
                user.SurName = appUserEditDto.SurName;
                user.PhoneNumber = appUserEditDto.PhoneNumber;
                user.City = appUserEditDto.City;
                user.District = appUserEditDto.District;
                user.ImageUrl = "Test";
                user.Email = appUserEditDto.Email;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }
    }
}
