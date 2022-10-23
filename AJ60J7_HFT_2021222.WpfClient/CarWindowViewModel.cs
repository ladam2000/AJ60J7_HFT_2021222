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
    public class CarWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Car> Cars { get; set; }

        private Car selectedCar;

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                if (value != null)
                {
                    selectedCar = new Engine()
                    {
                        Type = value.Type,
                        Id = value.Id,
                        Horsepower = value.Horsepower
                    };
                    OnPropertyChanged();
                    (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreatCarCommand { get; set; }

        public ICommand DeleteCarCommand { get; set; }

        public ICommand UpdateCarCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public CarWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Cars = new RestCollection<Car>("http://localhost:44728/", "car");

                CreatCarCommand = new RelayCommand(() =>
                {
                    Cars.Add(new Engine()
                    {
                        Type = SelectedCar.Type,
                        Horsepower = SelectedCar.Horsepower

                    });
                });

                UpdateCarCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Cars.Update(SelectedCar);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteCarCommand= new RelayCommand(() =>
                {
                    Cars.Delete(SelectedCar.Id);
                },
                () =>
                {
                    return SelectedCar != null;
                });
                SelectedCar = new Car();
            }
        }
    }
}
