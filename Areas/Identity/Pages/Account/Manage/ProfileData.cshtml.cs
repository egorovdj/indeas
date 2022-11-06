// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace indeas.Areas.Identity.Pages.Account.Manage
{
    public class ProfileDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly Models.PlatformData _db;

        public ProfileDataModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = new Models.PlatformData();
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public Models.Person Input { get; set; }

        private async Task LoadAsync(IdentityUser user)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var db = new Models.PlatformData();

            Input = db.People.Include(p=>p.Competencies).SingleOrDefault(p => p.UserId == userId);
            if (Input == null)
            {
                Input = new Models.Person();
                Input.UserId = userId;
                db.People.Add(Input);
                db.SaveChanges();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с идентификатором '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostSaveProfileAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с идентификатором '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);

            var person = _db.People.SingleOrDefault(p => p.UserId == userId);
            person.Nic = Input.Nic;
            person.Name = Input.Name;
            person.MiddleName = Input.MiddleName;
            person.Surname = Input.Surname;
            person.Contacts = Input.Contacts;
            person.AboutMe = Input.AboutMe;
            person.Gender = Input.Gender;
            person.Private = Input.Private;
            person.Moderation = Input.Moderation;
            _db.SaveChanges();
            return Page();
        }
    }
}
