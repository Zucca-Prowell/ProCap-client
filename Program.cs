﻿using Npgsql;
using Npgsql.Replication.PgOutput.Messages;
using Prowell_slack_bot;

namespace PROCAP_CLIENT
{
    internal static class Program
    {

        [STAThread]

        static void Main()
        {

            ApplicationConfiguration.Initialize();
            Application.Run(new Formmain());
            Formmain formmain = new Formmain();
            
        }
    }
}