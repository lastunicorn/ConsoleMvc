﻿// ConsoleMvc
// Copyright (C) 2017-2018 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.using System.Collections.Generic;

using DustInTheWind.ConsoleMvc.Demo.Controllers;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Mvc;

namespace DustInTheWind.ConsoleMvc.Demo
{
    internal static class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();
            RunDemo();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleMvc Demo");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows the usage of the MVC framework for Console.");
            CustomConsole.WriteLine();
        }

        private static void RunDemo()
        {
            ConsoleApplication consoleApplication = new ConsoleApplication();

            consoleApplication.Routes.AddRange(new[]
            {
                new Route("q", typeof(ExitController)),
                new Route("quit", typeof(ExitController)),
                new Route("exit", typeof(ExitController)),
                new Route("help", typeof(HelpController)),
                new Route("whale", typeof(WhaleController)),
                new Route("whales", typeof(WhaleController)),
                new Route("prompter", typeof(PrompterController))
            });

            consoleApplication.Run();
        }
    }
}