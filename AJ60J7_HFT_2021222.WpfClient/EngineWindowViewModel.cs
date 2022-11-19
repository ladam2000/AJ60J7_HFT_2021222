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
        public RestCollection<Engine> Engines { get; set; }

        private Engine selectedEngine;

        public Engine SelectedEngine
        {
            get { return selectedEngine; }
            set
            {
                if (value != null)
                {
                    selectedEngine = new Engine()
                    {
                        Type = value.Type,
                        Id = value.Id,
                        Horsepower = value.Horsepower
                    };
                    OnPropertyChanged();
                    (DeleteEngineCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateEngineCommand as RelayCommand).NotifyCanExecuteChanged();
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
                Engines = new RestCollection<Engine>("http://localhost:44728/", "engine","hub");
                try
                {
                    CreatEngineCommand = new RelayCommand(() =>
                    {
                        Engines.Add(new Engine()
                        {
                            Type = SelectedEngine.Type,
                            Horsepower = SelectedEngine.Horsepower

                        });
                    });

                    UpdateEngineCommand = new RelayCommand(
                    () => { Engines.Update(SelectedEngine); },
                        () => { return SelectedEngine != null; }
                    );

                    DeleteEngineCommand = new RelayCommand(() =>
                    {
                        Engines.Delete(SelectedEngine.Id);
                    },
                    () =>
                    {
                        return SelectedEngine != null;
                    });
                    SelectedEngine = new Engine();
                }
                catch (NullException e)
                {
                    MessageBox.Show(e.Message);
                }


            }
        }
    }
}
