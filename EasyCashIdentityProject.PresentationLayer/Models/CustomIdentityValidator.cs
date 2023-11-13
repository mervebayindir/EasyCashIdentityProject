using Microsoft.AspNetCore.Identity;

namespace EasyCashIdentityProject.PresentationLayer.Models
{
	public class CustomIdentityValidator : IdentityErrorDescriber
	{
		public override IdentityError PasswordTooShort(int length)  // şifrenin çok kısa olduğunu bildiren methoddur
		{
			return new IdentityError()
			{
				Code = "PasswordTooShort",  //Key değeridir ve methodun ismi yazılır.
				Description = $"Parola en az {length} karakter olmalıdır"
			};
		}
		public override IdentityError PasswordRequiresUpper()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresUpper",
				Description = "Lütfen en az 1 büyük harf giriniz"
			};
		}
		public override IdentityError PasswordRequiresLower()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresLower",
				Description = "Lütfen en az 1 küçük harf giriniz"
			};
		}
		public override IdentityError PasswordRequiresDigit()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresDigit",
				Description = " Lütfen en az 1 tane rakam giriniz"
			};
		}
		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresNonAlphanumeric",
				Description = "Lütfen en az 1 tane sembol giriniz"
			};
		}
		public override IdentityError DuplicateUserName(string userName)
		{
			return new IdentityError()
			{
				Code = "InvalidToken",
				Description = "Aynı isimde kullanıcı ismi vardır. Lütfen başka bir kullanıcı ismi giriniz"
			};
		}
	}
}
