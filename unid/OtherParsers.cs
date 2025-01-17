﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace unid
{
    class OtherParsers
    {
        public static void ParseEcho(string[] args) {
            args[0] = string.Empty;
            string ar = string.Empty;
            foreach (string arg in args)
            {
                ar += arg + " ";
            }
            if (ar.StartsWith(" "))
            {
                ar = ar.Substring(1);
            }
            Console.WriteLine(ar);
        }
        public static void ParseTypewrite(string[] args)
        {
            args[0] = string.Empty;
            string ar = string.Empty;
            foreach (string arg in args)
            {
                ar += arg + " ";
            }
            if (ar.StartsWith(" "))
            {
                ar = ar.Substring(1);
            }
            foreach (char ch in ar)
            {
                Thread.Sleep(100);
                Console.Write(ch);
            }
            Console.Write("\n");
        }
    }
    class readfile
    {
        public static void ParseRF(string[] args)
        {
            try
            {
                StreamReader rflib = new StreamReader(args[1]);
                string thefile = rflib.ReadToEnd();
                Console.WriteLine(thefile);
            }
            catch (Exception er)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(er.Message);
                Console.ResetColor();
            }
        }
    }
    class sudo
    {
        public static void ParseSudo(string[] args)
        {
            args[0] = string.Empty;
            string cm = string.Empty;
            foreach (string arg in args)
            {
                cm += arg;
            }
            try
            {
                bool isrunning = admininter.Elevate(cm);
                if (isrunning == true)
                {
                    Console.ResetColor();
                    Console.WriteLine("Elevated program.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not elevate program.");
                    Console.ResetColor();
                }
            }
            catch (Exception er)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(er.Message);
                Console.ResetColor();
            }
        }
    }
    class pkg
    {
        public static void ParsePkg(string[] args)
        {
            Console.Title = $"UNiD CMD - Package manager installing {args[2]}.";
            try
            {
                Console.WriteLine("Preparing package manager for use.");
                WebClient downloadapi = new WebClient();
                string user = "";
                foreach (string dir in Directory.GetDirectories(@"C:\Users\"))
                {
                    if (dir.Contains(Environment.UserName))
                    {
                        user = dir;
                    }
                }
                if (Directory.Exists(user + @"\UNiDPackages"))
                {
                    Console.WriteLine("Directory exists. Selecting package.");
                    if (args[2] == "py")
                    {
                        ParseDL(user, args, downloadapi);
                    }
                    else if (args[2] == "java")
                    {
                        ParseDL(user, args, downloadapi);
                    }
                    else if (args[2] == "node")
                    {
                        ParseDL(user, args, downloadapi);
                    }
                    else if (args[2] == "bulkimageresizer")
                    {
                        ParseDL(user, args, downloadapi);
                    }
                    else if (args[2] == "mictest")
                    {
                        ParseDL(user, args, downloadapi);
                    }
                    else if (args[2] == "register")
                    {
                        ParseDL(user, args, downloadapi);
                    }
                    else
                    {
                        Console.WriteLine("Unknown package. Try again, or use the list subcommand.");
                    }
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(user + @"\UNiDPackages");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("User information verified and directory created. Please retype the command.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("You won't have to do this again unless you switch accounts.");
                        Console.ResetColor();
                    }
                    catch (Exception er)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(er.Message);
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception er)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(er.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void ParseDL(string user, string[] args, WebClient downloadapi)
        {
            if (File.Exists(user + $@"\UNiDPackages\{args[2]}.exe"))
            {
                Console.WriteLine($"Overwriting your previous {args[2]} installation. You need to run the command again to continue installing.");
                File.Delete(user + $@"\UNiDPackages\{args[2]}.exe");
            }
            else
            {
                try
                {
                    if (args[2] == "node")
                    {
                        downloadapi.DownloadFile($"https://raw.githubusercontent.com/AcaiBerii/UNiD/raw/master/node.msi", user + $@"\UNiDPackages\node.msi");
                    }
                    else if (args[2] == "register")
                    {
                        downloadapi.DownloadFile("https://github.com/AcaiBerii/UNiD/releases/download/1.0.0-x32/register.zip", user + @"\UNiDPackages\register.zip");
                    }
                    else if (args[2] == "bulkimageresizer")
                    {
                        downloadapi.DownloadFile("https://github.com/AcaiBerii/BulkImageResizer/raw/master/v1.zip", user + @"\UNiDPackages\bulkimageresizer.zip");
                    }
                    else if (args[2] == "mictest")
                    {
                        downloadapi.DownloadFile("https://github.com/AcaiBerii/UNiD/raw/master/MicTest.zip", user + @"\UNiDPackages\mictest.zip");
                    }
                    else
                    {
                        downloadapi.DownloadFile($"https://raw.githubusercontent.com/AcaiBerii/UNiD/master/{args[2]}.exe", user + $@"\UNiDPackages\{args[2]}.exe");
                    }
                    Process.Start("explorer.exe", user + @"\UNiDPackages\");
                }
                catch (WebException er)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(er.Message);
                    Console.ResetColor();
                }
                Console.WriteLine(@$"{args[2]} installed.");
            }
        }
    }
}
