using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DtoLayer.Dtos.CustomerAccountProcessDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class SendMoneyController : Controller
    {
        private readonly ICustomerAccountProcessService _customerAccountProcessService;
        private readonly UserManager<AppUser> _userManager;

        public SendMoneyController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService)
        {
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
        }

        [HttpGet]
        public IActionResult Index(string mycurrency)
        {
            ViewBag.currency = mycurrency;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SendMoneyForCustomerAccountProcessDto sendMoneyForCustomerAccountProcessDto)
        {
            var context = new Context();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var receiverAccountNumber = context.CustomerAccounts
                .Where(x => x.CustomerAccountNumber == sendMoneyForCustomerAccountProcessDto.ReceiverAccountNumber)
                .Select(y => y.CustomerAccountID).FirstOrDefault();

            var senderAccountNumberID = context.CustomerAccounts.Where(x => x.AppUserID == user.Id)
                .Where(y => y.CustomerCurrency == "Türk Lirası")
                .Select(z => z.CustomerAccountID)
                .FirstOrDefault();

            var values = new CustomerAccountProcess()
            {
                ProcessDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                SenderID = senderAccountNumberID,
                ProcessType = "Havale",
                ReceiverID = receiverAccountNumber,
                Amount = sendMoneyForCustomerAccountProcessDto.Amount,
                Description=sendMoneyForCustomerAccountProcessDto.Description
            };

            _customerAccountProcessService.TInsert(values);
            return RedirectToAction("Index", "Deneme");
        }

    }
}
