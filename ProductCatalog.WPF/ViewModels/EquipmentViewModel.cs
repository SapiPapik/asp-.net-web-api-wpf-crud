using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using ProductCatalog.BLL.Contracts.Contracts;
using ProductCatalog.WPF.Models;

namespace ProductCatalog.WPF.ViewModels {
    public class EquipmentViewModel : ViewModelBase {

        private EquipmentModel _equipment;
        private ICollection<EquipmentModel> _equipments;
        private IBaseEquipmentService _equipmentService;
        private IBrandService _brandService;
        private IToolTypeService _toolTypeService;

        public static async Task<EquipmentViewModel> Create() {
            var equipmentService = BootStrapper.Resolve<IBaseEquipmentService>();
            var brandService = BootStrapper.Resolve<IBrandService>();
            var toolTypeService = BootStrapper.Resolve<IToolTypeService>();
            var model = new EquipmentViewModel() {
                _equipment = new EquipmentModel(),
                _equipmentService = equipmentService,
                _brandService = brandService,
                _toolTypeService = toolTypeService,
                _equipments = Mapper.Map<ICollection<EquipmentModel>>(await equipmentService.GetAllAsync())
            };

            return model;
        }

        public string Specifications {
            get => _equipment.Specifications;
            set {
                _equipment.Specifications = value;
                OnPropertyChanged();
            }
        }

        public decimal Price {
            get => _equipment.Price;
            set {
                _equipment.Price = value;
                OnPropertyChanged();
            }
        }

        public string BrandName {
            get => _equipment.BrandName;
            set {
                _equipment.BrandName = value;
                OnPropertyChanged();
            }
        }

        public string ToolTypeName {
            get => _equipment.ToolTypeName;
            set {
                _equipment.ToolTypeName = value;
                OnPropertyChanged();
            }
        }

        private void OnDbDoubleClick(object parameter) {
            if (parameter is EquipmentModel equipment) {
                Specifications = equipment.Specifications;
                Price = equipment.Price;
                BrandName = equipment.BrandName;
                ToolTypeName = equipment.ToolTypeName;
            }
        }
    }
}
