// ConsoleMvc
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
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

// --------------------------------------------------------------------------------
// Bugs or fearure requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Collections.Generic;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.CommandProviders;

namespace DustInTheWind.ConsoleMvc
{
    /// <summary>
    /// Represents the console application that processes commands.
    /// </summary>
    public class ConsoleApplication
    {
        private readonly Router router;
        private ICommandProvider commandProvider;

        public ICommandProvider CommandProvider
        {
            get { return commandProvider; }
            set
            {
                if (commandProvider != null)
                    commandProvider.NewCommand -= HandleNewCommand;

                commandProvider = value;

                if (commandProvider != null)
                    commandProvider.NewCommand += HandleNewCommand;

                router.CommandProvider = commandProvider;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IServiceProvider"/> that is used to create the controllers.
        /// </summary>
        public IServiceProvider ServiceProvider
        {
            get { return router.ServiceProvider; }
            set { router.ServiceProvider = value; }
        }

        /// <summary>
        /// Gets the routes used to map a command to a controller that will be executed.
        /// </summary>
        public List<Route> Routes => router.Routes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
        /// </summary>
        public ConsoleApplication()
        {
            router = new Router
            {
                ConsoleApplication = this
            };
        }

        /// <summary>
        /// Starts to process commands.
        /// This method blocks until the application is stopped.
        /// </summary>
        public void Run()
        {
            if (CommandProvider == null)
                CommandProvider = new Prompter();

            CommandProvider.Run();
        }

        /// <summary>
        /// Stops processing commands and exits the <see cref="Run"/> method.
        /// </summary>
        public void Exit()
        {
            CommandProvider.RequestStop();
        }

        private void HandleNewCommand(object sender, NewCommandEventArgs e)
        {
            try
            {
                IController controller = router.CreateController(e.Command);
                controller.Execute(e.Command.Parameters);
            }
            catch (MissingRouteException ex)
            {
                CustomConsole.WriteLineError(ConsoleApplicationResources.UnknownCommandMessage, ex.Command);
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
            }
            finally
            {
                CustomConsole.WriteLine();
            }
        }
    }
}