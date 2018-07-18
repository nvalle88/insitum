using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;

namespace insitum.Utiles
{
    public static class EnviarCorreo
    {
        public static void Enviar(string Email, string Subject, string Body)
        {
            
            WebMail.SmtpServer = CorreoUtil.SmtpServer;
            WebMail.SmtpPort = Convert.ToInt32(CorreoUtil.Port);
            WebMail.SmtpUseDefaultCredentials = CorreoUtil.SmtpUseDefaultCredentials;
            WebMail.EnableSsl = CorreoUtil.EnableSsl;
            WebMail.UserName = CorreoUtil.UserName;
            WebMail.Password = CorreoUtil.Password;
            WebMail.From = CorreoUtil.UserName;
            WebMail.Send(to: Email, subject: Subject, body: Body, isBodyHtml: true);
        }

        public static void Enviar(string Email, string Subject, string Body,bool isBodyHtml)
        {
            WebMail.SmtpServer = CorreoUtil.SmtpServer;
            WebMail.SmtpPort = Convert.ToInt32(CorreoUtil.Port);
            WebMail.SmtpUseDefaultCredentials = CorreoUtil.SmtpUseDefaultCredentials;
            WebMail.EnableSsl = CorreoUtil.EnableSsl;
            WebMail.UserName = CorreoUtil.UserName;
            WebMail.Password = CorreoUtil.Password;
            WebMail.From = CorreoUtil.UserName;
            WebMail.Send(to: Email, subject: Subject, body: Body, isBodyHtml: isBodyHtml);
        }

        internal static void Enviar(object email, string creacionCuentaCliente, object p)
        {
            throw new NotImplementedException();
        }
    }
}