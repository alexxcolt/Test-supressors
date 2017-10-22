using Client;
using Client.View;
using Client.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Suppressors
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();

            container.RegisterType<IBusinessContext, BusinessContext>();
            container.RegisterType<ViewModel>();

            var window = new MainWindow
            {
                DataContext = container.Resolve<ViewModel>()
            };

            window.ShowDialog();
        }
    }
}
