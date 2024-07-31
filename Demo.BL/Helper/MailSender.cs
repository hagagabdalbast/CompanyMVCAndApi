﻿using Demo.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Helper
{
    public static class MailSender
    {
        public static string SendMail(MailVM model)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("hagag@gmail.com", "@@@AAA321_321");
                smtp.Send("hagag@gmail.com", model.Mail, model.Title, model.Message);

                var result = "Mail Sent Successfully";
                return result;
            }
            catch (Exception ex)
            {
                var result = "Faild";
                return result;
            }
        }
    }
}
