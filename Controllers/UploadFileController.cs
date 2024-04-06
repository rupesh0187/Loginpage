using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;

namespace Loginpage.Controllers
{
    public class UploadFileController : Controller
    {
        string mailBody = "<!DOCTYPE html>" +
                            "<html>" +
                            "<body style=\"background -color:#ff7f26;text-align:center;\">" +
                            "<h1 style=\"color:#051a80;\"> EMAIL NOTIFICATION  TO PROJECT COORDINATORS(PC)  </h1>" +
                            "<h2 style=\"color:fff;\"> Please find the attachment files.  </h2>" +
                            "<label style=\"color:orange;font-size:100px;border:5px dotted;border-radius:5px;\"> EMAIL NOTIFICATION </label>" +
                            "</body> " +
                            "</html>";
        string mailTitle = "Attachment Demo";
        string mailSubject = "Email with Attachment";
        string fromEmail = "ranjanrupesh0187@gmail.com";
        string mailPassword = "zrvb rjdm ynox gxql";


        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(string toEmail, IFormFile fileToAttach)
        {
            try
            {
                MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(toEmail));

                message.Subject = mailSubject;
                message.Body = mailBody;
                message.IsBodyHtml = true;

                message.Attachments.Add(new Attachment(fileToAttach.OpenReadStream(), fileToAttach.FileName));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
                credential.UserName = fromEmail;
                credential.Password = mailPassword;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Send(message);

                ViewBag.emailsentmessage = "Email sent successfully";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.emailsentmessage = "Failed to send email: " + ex.Message;
            }

            return View();
        }

       
    }
}
