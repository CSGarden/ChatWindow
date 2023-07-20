// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using System.Configuration;
using ChatWindow.AppClasses;
using ChatApi;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatWindow {
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App() {
            this.InitializeComponent(); 
        }
        private Window m_window;

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args) {
            await AppSettings.Init();
            Services = ConfigureServices();
            m_window = new MainWindow();
            m_window.Activate();

        }


        internal static IServiceProvider Services { get; private set; }
        private static IServiceProvider ConfigureServices() {
            var services = new ServiceCollection();

            services.AddTransient<ChatApiOption>();
            services.AddTransient<ChatBehaviorOption>();
            services.AddTransient<ChatApiService>(sp => new ChatApiService(new ChatApiOption(AppSettings.Key, AppSettings.URL), new ChatBehaviorOption()));
            services.AddSingleton<ChatApiHelper>();

            return services.BuildServiceProvider();
        }

    }
}
