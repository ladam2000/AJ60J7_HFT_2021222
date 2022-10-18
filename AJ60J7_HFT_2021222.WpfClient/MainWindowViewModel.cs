﻿using AJ60J7_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AJ60J7_HFT_2021222.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Car> Cars { get; set; }
        public RestCollection<Brand> Brands { get; set; }
        public RestCollection<Engine> Engines { get; set; }


        private Brand selectedBrand;

        private Car selectedCar;






        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set 
            {
                if (value != null)
                {
                    selectedBrand = new Brand()
                    {
                        Name = value.Name,
                        Id = value.Id
                    };
                   OnPropertyChanged();
                   (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                if (value != null)
                {
                    
                    selectedCar = new Car()
                    {
                        Model = value.Model,
                        BasePrice = value.BasePrice,
                        Id = value.Id,
                        Engine = new Engine()
                        {
                            Horsepower = value.Engine.Horsepower,
                            Type = value.Engine.Type,
                            Id = value.Engine.Id
                            
                           
                        }
                    };
                    OnPropertyChanged();
                    (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

       
        


        public ICommand CreatBrandCommand { get; set; }

        public ICommand DeleteBrandCommand { get; set; }

        public ICommand UpdateBrandCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            
            if (!IsInDesignMode)
            {
                Brands = new RestCollection<Brand>("http://localhost:44728/", "brand");
                Cars = new RestCollection<Car>("http://localhost:44728/", "car");
                Engines = new RestCollection<Engine>("http://localhost:44728/", "engine");

                CreatBrandCommand = new RelayCommand(() =>
                {
                    Brands.Add(new Brand() 
                    {
                        Name = SelectedBrand.Name
                    });
                });

                UpdateBrandCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Brands.Update(SelectedBrand);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteBrandCommand = new RelayCommand(()=> 
                {
                    Brands.Delete(SelectedBrand.Id);
                },
                () =>
                {
                    return SelectedBrand != null;
                });
                SelectedBrand = new Brand();
            }
        }
    }
}
