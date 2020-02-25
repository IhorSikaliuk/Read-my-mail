using System;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;

namespace Read_my_mail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string server = "imap.ukr.net";
            int port = 993;
            string login = "aw33@ukr.net";
            int numberOfMails = 3;  //Кількість повідомлення для читання
            Console.Write("Password: ");
            string password = Console.ReadLine();
            ImapClient client = new ImapClient();
            
            try
            {
                client.Connect (server, port, useSsl: true);    //Підключення до серверу та аунтифікація
                client.Authenticate (login, password);
                if (client.IsAuthenticated)
                    Console.WriteLine("Authentication successful");

                IMailFolder inbox = client.Inbox;   //Отримання папки повідомлень "Вхідні"
                inbox.Open (FolderAccess.ReadOnly);
                if (inbox.Count < numberOfMails)
                    numberOfMails = inbox.Count;
                
                for (int i = (inbox.Count - 1); i > (inbox.Count - 1 - numberOfMails); i--) //Читання повідомлень
                {
                    MimeMessage message = inbox.GetMessage(i);
                    Console.WriteLine($"\nFrom: {message.From}");
                    Console.WriteLine($"Date: {message.Date}");
                    Console.WriteLine($"Title: {message.Subject}");
                    Console.WriteLine(message.TextBody);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine (e.Message);
            }

            client.Disconnect (true);
            Console.ReadKey ();
        }
    }
}
