using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using ProductCatalog.WPF.Commands;

namespace ProductCatalog.WPF.ViewModels {
    public class MainPageViewModel : ViewModelBase {

        private EquipmentForm equipmentForm;

        public MainPageViewModel() {
            EquipmentCommand = new RelayCommand(EquipmentFormShow);
        }

        public ICommand EquipmentCommand {
            get;
            private set;
        }

        public ICommand ToolTypeCommand {
            get;
            private set;
        }

        public ICommand BrandCommand {
            get;
            private set;
        }

        private void EquipmentFormShow(object parameter) {
            if (equipmentForm == null) {
                equipmentForm = new EquipmentForm();
            }

            equipmentForm.Show();
        }
    }
}
