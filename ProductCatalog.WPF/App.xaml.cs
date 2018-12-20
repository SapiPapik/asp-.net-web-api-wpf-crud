using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows;
using ProductCatalog.BLL.Contracts.Contracts;
using ProductCatalog.WPF.ViewModels;

namespace ProductCatalog.WPF {
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application {

        private readonly CompositeDisposable _disposable;

        public App()
        {
            _disposable = new CompositeDisposable();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            BootStrapper.Start();
            //var window = EquipmentViewModel.Create().Result;
            var window = new EquipmentForm();

            window.Closed += (s, a) =>
            {
                _disposable.Dispose();
                BootStrapper.Stop();
            };

            window.Show();
        }
    }
}
