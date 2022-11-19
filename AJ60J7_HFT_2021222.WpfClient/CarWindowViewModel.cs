using AJ60J7_HFT_2021222.Models;
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
        //private string errorMessage;

        //public string ErrorMessage
        //{
        //    get { return errorMessage; }
        //    set { SetProperty(ref errorMessage, value); }
        //}

        public RestCollection<Car> Cars { get; set; }

        private Car selectedCar;

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                SetProperty(ref selectedCar, value);
                (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateCarCommand as RelayCommand).NotifyCanExecuteChanged();
                //if (value != null)
                //{
                //    selectedCar = new Car()
                //    {
                //        Model = value.Model,
                //        Id = value.Id,
                //        BasePrice = value.BasePrice
                //    };
                //    OnPropertyChanged();
                //    (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
                //}
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
                    Cars.Add(new Car()
                    {
                        Model = SelectedCar.Model,
                        BasePrice = SelectedCar.BasePrice

                    });
                });

                //UpdateCarCommand = new RelayCommand(() =>
                //{
                //    try
                //    {
                //        Cars.Update(SelectedCar);
                //    }
                //    catch (ArgumentException ex)
                //    {
                //        ErrorMessage = ex.Message;
                //    }

                //});
                UpdateCarCommand = new RelayCommand(
                    () => { Cars.Update(SelectedCar); },
                        () => { return SelectedCar != null; }
                );

                DeleteCarCommand = new RelayCommand(() =>
                {
                    //ez a gomb fejlesztés alatt áll :D
                    Cars.Delete(SelectedCar.Id);
                },
                () =>
                {
                    return SelectedCar != null;
                });
                //SelectedCar = new Car();
            }
        }
    }
}
