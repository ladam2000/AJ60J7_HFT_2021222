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
    public class EngineWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Engine> Engines { get; set; }

        private Engine selectedEngine;

        public Engine SelectedBrand
        {
            get { return selectedEngine; }
            set
            {
                if (value != null)
                {
                    selectedEngine = new Engine()
                    {
                        Type = value.Type,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteEngineCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreatEngineCommand { get; set; }

        public ICommand DeleteEngineCommand { get; set; }

        public ICommand UpdateEngineCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public EngineWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Engines = new RestCollection<Engine>("http://localhost:44728/", "engine");

                CreatEngineCommand = new RelayCommand(() =>
                {
                    Engines.Add(new Engine()
                    {
                        Type = SelectedBrand.Type
                    });
                });

                UpdateEngineCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Engines.Update(SelectedBrand);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteEngineCommand = new RelayCommand(() =>
                {
                    Engines.Delete(SelectedBrand.Id);
                },
                () =>
                {
                    return SelectedBrand != null;
                });
                SelectedBrand = new Engine();
            }
        }
    }
}
