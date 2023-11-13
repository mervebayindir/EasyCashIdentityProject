using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class RegisterController : Controller
	{
		private readonly UserManager<AppUser> _userManager;  // UserManager ıdentity kütüphanesi ile birlikte kullanılır

		public RegisterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
		{
			if (ModelState.IsValid)
			{
				Random random = new Random();
				int code;
				code = random.Next(100000, 1000000);
				AppUser appUser = new AppUser()
				{
					UserName = appUserRegisterDto.UserName,
					Name = appUserRegisterDto.Name,
					SurName = appUserRegisterDto.SurName,
					Email = appUserRegisterDto.Email,
					City = "aaa",
					District = "bbb",
					ImageUrl = "ccc",
					ConfirmCode = code
				};
				var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
				if (result.Succeeded)
				{
					MimeMessage mimeMessage = new MimeMessage();  // Maile onay esajı atmak için yazılır
					MailboxAddress mailboxAddressForm = new MailboxAddress("Easy Cash Admin", "kutluhanbayindir120@gmail.com");
					MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);

					mimeMessage.From.Add(mailboxAddressForm);
					mimeMessage.To.Add(mailboxAddressTo);

					var bodyBuilder = new BodyBuilder();
					bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + code;
					mimeMessage.Body = bodyBuilder.ToMessageBody();
					mimeMessage.Subject = "Easy Cash Onay Kodu";


					SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("kutluhanbayindir120@gmail.com", "uijc dklv kxou kvof");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

					TempData["Mail"] = appUserRegisterDto.Email;

                    return RedirectToAction("Index", "ConfirmMail");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			return View();
		}
	}
}

/*
 Identity kütüphanesinde şifrenin bir formatı vardır;  Bur şartlardan istenilmeyen kaldırılabilir
 En az 6 karakter karakter olmalı
 En az 1 küçük harf içermeli  
 En az 1 büyük harf içermeli
 En az 1 sembol içermeli
 En az 1 sayı içermeli
 */
