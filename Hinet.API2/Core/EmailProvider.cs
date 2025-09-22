using CommonHelper.ObjectExtention;
using CommonHelper.String;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace Web.Common
{
    public class EmailProvider
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmailProvider));
        private static string PathRoot = WebConfigurationManager.AppSettings["PathRoot"];
        private static string MailFrom = WebConfigurationManager.AppSettings["MailFrom"];
        private static string MailHost = WebConfigurationManager.AppSettings["MailHost"];
        private static string MailAlias = WebConfigurationManager.AppSettings["MailAlias"];
        private static string MailPort = WebConfigurationManager.AppSettings["MailPort"];
        private static string MailUserName = WebConfigurationManager.AppSettings["MailUserName"];
        private static string MailPassword = WebConfigurationManager.AppSettings["MailPassword"];
        private static string MailEnableSsl = WebConfigurationManager.AppSettings["MailEnableSsl"];
        private static string AllowSendMail = WebConfigurationManager.AppSettings["AllowSendMail"];
        private static string AllowMailList = WebConfigurationManager.AppSettings["AllowMailList"];

        private static bool checkSettingAllowSendMail(string address)
        {
            if (AllowSendMail == "true")
            {
                return true;
            }
            else
            {
                var listAllow = AllowMailList.Split(';').ToList();
                var IsAllow = listAllow.Contains(address);
                return IsAllow;
            }
            //return false;
        }

        public static bool sendEmail(string body, string subject, List<string> address, List<string> addressCC)
        {
            log.Info("Bắt đầu gửi mail :" + subject + " đển " + string.Join(",", address));

            SmtpClient server = new SmtpClient();
            try
            {
                server.Host = MailHost;
                server.Port = MailPort.ToIntOrZero();

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(MailFrom, MailAlias);
                foreach (var item in address)
                {
                    if (!checkSettingAllowSendMail(item))
                    {
                        log.Info("Hệ thống đã tắt chức năng gửi email " + item);
                        return true;
                    }
                    else
                    {
                        mail.To.Add(item);
                    }
                }
                if (addressCC != null && addressCC.Any())
                {
                    var listCC = addressCC.Where(x => !address.Contains(x)).ToList();
                    if (listCC != null && listCC.Any())
                    {
                        foreach (var itemcc in listCC)
                        {
                            if (!checkSettingAllowSendMail(itemcc))
                            {
                                log.Info("Hệ thống đã tắt chức năng gửi email " + itemcc);
                                return true;
                            }
                            else
                            {
                                mail.CC.Add(itemcc);
                            }
                        }
                    }
                }

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                server.Credentials = new NetworkCredential(MailUserName, MailPassword);
                server.EnableSsl = MailEnableSsl == "true";
                server.Send(mail);
                log.Info("Gửi mail thành công :" + subject + " đển " + string.Join(",", address));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Gửi mail thất bại :" + subject + " đển " + string.Join(",", address), ex);
                return false;
            }
        }

        public static bool sendEmailSingle(string body, string subject, string address, string addressCC = null)
        {
            log.Info("Bắt đầu gửi mail :" + subject + " đển " + string.Join(",", address));
            if (!checkSettingAllowSendMail(address))
            {
                log.Info("Hệ thống đã tắt chức năng gửi email " + address);
                address = "hoanvudev@gmail.com";
                addressCC = "hoanvudev@gmail.com";
                //return true;
            }
            SmtpClient server = new SmtpClient();
            try
            {
                server.Host = MailHost;
                server.Port = MailPort.ToIntOrZero();

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(MailFrom, MailAlias);

                mail.To.Add(address);

                if (!string.IsNullOrEmpty(addressCC) && address != addressCC)
                {
                    mail.CC.Add(addressCC);
                }

                mail.Subject = subject;
                mail.Body = AddLayout(body);
                mail.IsBodyHtml = true;
                server.Credentials = new NetworkCredential(MailUserName, MailPassword);
                server.EnableSsl = MailEnableSsl == "true";
                server.Send(mail);
                log.Info("Gửi mail thành công :" + subject + " đển " + string.Join(",", address));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Gửi mail thất bại :" + subject + " đển " + address, ex);
                return false;
            }
        }

        public bool sendEmailSingleHangfire(string body, string subject, string address, string addressCC = null)
        {
            log.Info("Bắt đầu gửi mail :" + subject + " đển " + string.Join(",", address));
            if (!checkSettingAllowSendMail(address))
            {
                log.Info("Hệ thống đã tắt chức năng gửi email " + address);
                address = "hoanvudev@gmail.com";
                addressCC = "hoanvudev@gmail.com";
            }
            SmtpClient server = new SmtpClient();
            try
            {
                server.Host = MailHost;
                server.Port = MailPort.ToIntOrZero();

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress(MailFrom, MailAlias);

                mail.To.Add(address);

                if (!string.IsNullOrEmpty(addressCC) && address != addressCC)
                {
                    mail.CC.Add(addressCC);
                }

                mail.Subject = subject;
                mail.Body = AddLayout(body);
                mail.IsBodyHtml = true;
                server.Credentials = new NetworkCredential(MailUserName, MailPassword);
                server.EnableSsl = MailEnableSsl == "true";
                server.Send(mail);
                log.Info("Gửi mail thành công :" + subject + " đển " + string.Join(",", address));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Gửi mail thất bại :" + subject + " đển " + address, ex);
                return false;
            }
        }

        /// <summary>
        /// Gửi email cho nhiều người
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool sendMultiEmailSingleHangfire(string body, string subject, string address)
        {
            log.Info("Bắt đầu gửi mail :" + subject + " đển " + address);
            var listEmail = address.Split(',').ToList();
            var listToSend = new List<string>();
            foreach (var item in listEmail)
            {
                if (!checkSettingAllowSendMail(item))
                {
                    log.Info("Hệ thống đã tắt chức năng gửi email " + address);
                }
                else
                {
                    listToSend.Add(item);
                }
            }

            if (listToSend != null && listToSend.Any())
            {
                SmtpClient server = new SmtpClient();
                try
                {
                    server.Host = MailHost;
                    server.Port = MailPort.ToIntOrZero();

                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.From = new MailAddress(MailFrom, MailAlias);
                    foreach (var mailSend in listToSend)
                    {
                        mail.To.Add(mailSend);
                    }

                    mail.Subject = subject;
                    mail.Body = AddLayout(body);
                    mail.IsBodyHtml = true;
                    server.Credentials = new NetworkCredential(MailUserName, MailPassword);
                    server.EnableSsl = MailEnableSsl == "true";
                    server.Send(mail);
                    log.Info("Gửi mail thành công :" + subject + " đển " + string.Join(",", address));
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("Gửi mail thất bại :" + subject + " đển " + address, ex);
                    return false;
                }
            }
            return true;
        }

        private static string AddLayout(string content)
        {
            var layout = File.ReadAllText(Path.Combine(PathRoot, "MailTemplate/layoutCommon.html"));
            if (!string.IsNullOrEmpty(layout))
            {
                layout = layout.Replace("[{ContentData}]", content);
                layout = layout.Replace("[{DateData}]", DateTime.Now.GetTextDisplay());
                return layout;
            }
            return content;
        }

        public static List<BindingKey> GetListKeyWithMailModel(string mailModel)
        {
            var model = new List<BindingKey>();
            try
            {
                var type = Type.GetType(mailModel);
                if (type != null)
                {
                    var getProperty = type.GetProperties();
                    foreach (var item in getProperty)
                    {
                        var name = item.Name;
                        var objdata = item.GetAttribute<DisplayNameAttribute>(false);
                        if (objdata != null)
                        {
                            name = objdata.DisplayName;
                        }
                        model.Add(new BindingKey()
                        {
                            Name = name,
                            Key = item.Name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new List<BindingKey>();
            }
            return model;
        }

        public static string BindingDataToMailContent<T>(object dataObj, string content) where T : class
        {
            if (!string.IsNullOrEmpty(content))
            {
                var listkey = Regex.Matches(content, @"\{\{[a-zA-Z0-9]{3,}\}\}");

                if (listkey != null && listkey.Count > 0)
                {
                    foreach (var item in listkey)
                    {
                        var propertyMath = Regex.Match(item.ToString(), @"^\{\{(?<pkey>[a-zA-Z0-9]{3,})\}\}$");
                        if (propertyMath.Success)
                        {
                            var propertyName = propertyMath.Groups["pkey"].ToString();

                            if (!string.IsNullOrEmpty(propertyName))
                            {
                                var valueProperty = string.Empty;
                                var data = dataObj as T;
                                var property = typeof(T).GetProperty(propertyName);
                                if (property != null)
                                {
                                    if (property.GetValue(data, null) != null)
                                    {
                                        valueProperty = property.GetValue(data, null).ToString();
                                    }
                                }
                                content = content.Replace(item.ToString(), valueProperty);
                            }
                        }
                        else
                        {
                            content = content.Replace(item.ToString(), string.Empty);
                        }
                    }
                }
            }
            return content;
        }

        //public static void SendMailForgotPassword(AppUser userDto)
        //{
        //    try
        //    {
        //        var body = ViewRenderer.RenderView("~/Views/Home/ForgotPassword.cshtml", new UserInfor() { UserName = userDto.UserName });
        //        var subject = "Quên mật khẩu";
        //        Task.Run(() => sendEmailSingleCustom(body, subject, userDto.Email).ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //    }
        //}
        //public static void SendMailRegister(AppUser userDto, string password)
        //{
        //    try
        //    {
        //        var body = ViewRenderer.RenderView("~/Views/Home/RegisterAccount.cshtml",
        //            new UserRegister() {
        //                UserName = userDto.UserName,
        //                FullName = userDto.FullName,
        //                Email = userDto.Email,
        //                Address = userDto.Address,
        //                Password = password,
        //                BirthDay = userDto.BirthDay,
        //                Gender = userDto.Gender,
        //                PhoneNumber = userDto.PhoneNumber
        //            });
        //        var subject = "Đăng ký tài khoản";
        //        Task.Run(() => sendEmailSingleCustom(body, subject, userDto.Email).ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //    }
        //}
        private static bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }

        public static bool sendEmailSingleCustom(string body, string subject, string address)
        {
            log.Info("Bắt đầu gửi mail :" + subject + " đển " + string.Join(",", address));
            //if (!checkSettingAllowSendMail(address))
            //{
            //    log.Info("Hệ thống đã tắt chức năng gửi email " + address);
            //    return true;
            //}
            try
            {
                MailMessage msg = new MailMessage(MailUserName, address);
                msg.Subject = subject.Replace('\r', ' ').Replace('\n', ' ');
                msg.Body = body;
                msg.IsBodyHtml = true;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.Priority = MailPriority.Normal;

                //Added this line here
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);

                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential networkCredential = new NetworkCredential(MailUserName, MailPassword);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;

                smtp.Send(msg);

                log.Info("Gửi mail thành công :" + subject + " đển " + string.Join(",", address));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Gửi mail thất bại :" + subject + " đển " + address, ex);
                return false;
            }
        }

        public class BindingKey
        {
            public string Name { get; set; }
            public string Key { get; set; }
        }
    }
}