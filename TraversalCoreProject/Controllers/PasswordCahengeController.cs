
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Threading.Tasks;
using TraversalCoreProject.Models;

namespace TraversalCoreProject.Controllers
{
    public class PasswordCahengeController : Controller
    {
        private readonly IAuthService _authService;

        public PasswordCahengeController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            var user = await _authService.GetByEmailAsync(forgetPasswordViewModel.Mail);
            if (user == null)
            {
                return View(forgetPasswordViewModel);
            }

            string passwordResetToken = await _authService.GeneratePasswordResetTokenAsync(user.Id);
            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChanges", new
            {
                userId = user.Id,
                token = passwordResetToken
            }, HttpContext.Request.Scheme);
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "emirhanhacioglu372@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", forgetPasswordViewModel.Mail);
            mimeMessage.To.Add(mailboxAddressTo);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = passwordResetToken;
            mimeMessage.Body=bodyBuilder.ToMessageBody();
            
            mimeMessage.Subject = "Şifre Değişiklik Talebi";
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("emirhanhacioglu372@gmail.com", "");
            client.Send(mimeMessage);
            client.Disconnect(true);

            
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string userid ,string token)
        {
            TempData["userid"] = userid;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userid = TempData["userid"];
            var token = TempData["token"];
            if (userid == null || token == null)
            { 
            }
            if (!int.TryParse(userid.ToString(), out var userId))
            {
                return View();
            }

            var result = await _authService.ResetPasswordAsync(userId, token.ToString(), resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }
    }
    
}
