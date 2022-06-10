//*********************************************************  
//  
// Copyright (c) Microsoft. All rights reserved.  
// This code is licensed under the MIT License (MIT).  
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF  
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY  
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR  
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.  
//  
//********************************************************* 
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SystrayComponent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            // debug
            using (StreamWriter file = new StreamWriter(@"c:\temp\SystrayComponent.log", append: true))
            {
                file.WriteLine(string.Join(",", args));
            }

            var isExpired = args[args.Length - 1].Equals("expired");

            Mutex mutex = null;
            if (!Mutex.TryOpenExisting("MySystrayExtensionMutex", out mutex))
            {
                mutex = new Mutex(false, "MySystrayExtensionMutex");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SystrayApplicationContext(isExpired));
                mutex.Close();
            }
        }
    }
}
