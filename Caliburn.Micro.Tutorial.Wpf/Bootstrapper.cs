﻿using Caliburn.Micro.Tutorial.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Caliburn.Micro.Tutorial.Wpf
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
            StartDebugLogger(); 
        }

        /// <summary>
        /// The logger will use the output window in visual studio to display
        /// logging info it collects from Caliburn.Micro. By default it uses 
        /// the implementation DebugLog.cs in the Caliburn.Micro library. Alter-
        /// natively, you can create your own version.
        /// </summary>
        public static void StartDebugLogger()
        {
            LogManager.GetLog = type => new DebugLog(type);
        }

        protected override void Configure()
        {
            _container.Instance(_container);
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            foreach(var assembly in SelectAssemblies())
            {
                assembly.GetTypes()
                    .Where(type => type.IsClass)
                    .Where(type => type.Name.EndsWith("ViewModel"))
                    .ToList()
                    .ForEach(viewModelType => _container.RegisterPerRequest(
                        viewModelType, viewModelType.ToString(), viewModelType));
            }
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync(typeof(ShellViewModel));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}