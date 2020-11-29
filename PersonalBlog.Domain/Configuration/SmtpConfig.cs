using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlog.Domain.Configuration
{
    //
    // Summary:
    //     The SMTP server's config
    public class SmtpConfig
    {
        public SmtpConfig() { }

        //
        // Summary:
        //     The host name to connect to.
        public string Server { get; set; }
        //
        // Summary:
        //     The optional user name.
        public string Username { get; set; }
        //
        // Summary:
        //     The optional password.
        public string Password { get; set; }
        //
        // Summary:
        //     The port to connect to. If the specified port is 0, then the default port will
        //     be used.
        public int Port { get; set; }
        //
        // Summary:
        //     The local domain is used in the HELO or EHLO commands sent to the SMTP server.
        //     If left unset, the local IP address will be used instead.
        public string LocalDomain { get; set; }
        //
        // Summary:
        //     If you set this value to true, the `SendEmailAsync` method won't send this email
        //     to the recipient and saves its content as an .eml file in the `PickupFolder`.
        //     Later you can open this file using Outlook or your web browser to debug your
        //     program.
        public bool UsePickupFolder { get; set; }
        //
        // Summary:
        //     An optional path to save emails on the local HDD. Its value will be used if you
        //     set the `UsePickupFolder` to true.
        public string PickupFolder { get; set; }
        //
        // Summary:
        //     The name of the mailbox.
        public string FromName { get; set; }
        //
        // Summary:
        //     The address of the mailbox.
        public string FromAddress { get; set; }
    }
}
