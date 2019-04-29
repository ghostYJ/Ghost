using System.Net.Mail;
using System.Text;

namespace Ghost.Utility
{
    /// <summary>
    /// MailMessage邮件信息扩展方法类
    /// </summary>
    public static class MailMessageExtensionMethod
    {  /// <summary>
       /// 转换成字符串
       /// </summary>
       /// <param name="mail"></param>
       /// <returns></returns>
        public static string ToStringExt(this MailMessage mail)
        {
            return string.Format(@"
Subject:{0}
Body:{1}
IsBodyHtml:{2}
To:{3}
CC:{4}
Bcc:{5}
From:{6}
Attachments:{7}"
                , mail.Subject
                , mail.Body
                , mail.IsBodyHtml
                , mail.To.ToStringExt()
                , mail.CC.ToStringExt()
                , mail.Bcc.ToStringExt()
                , mail.From.ToStringExt()
                , mail.Attachments.ToStringExt()
            );
        }

        /// <summary>
        /// 邮件地址集合ToString()
        /// </summary>
        /// <param name="mailAddressCollection"></param>
        /// <returns></returns>
        private static string ToStringExt(this MailAddressCollection mailAddressCollection)
        {
            if (mailAddressCollection == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (MailAddress item in mailAddressCollection)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(item.ToStringExt());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 邮件地址ToString()
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private static string ToStringExt(this MailAddress address)
        {
            if (address == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(address.DisplayName))
                sb.Append("\"" + address.DisplayName + "\" ");
            sb.Append("<" + address.Address + ">");
            return sb.ToString();
        }

        /// <summary>
        /// 邮件附件ToString()
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        private static string ToStringExt(this AttachmentCollection ac)
        {
            if (ac == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (Attachment attachment in ac)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(attachment.Name);
            }
            return sb.ToString();
        }
    }
}
