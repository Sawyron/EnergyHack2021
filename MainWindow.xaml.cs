using EnergyHack2021.Core;
using EnergyHack2021.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EnergyHack2021
{

    public partial class MainWindow : Window
    {
        private BindingList<RecomendationModel> _recomendationsList = new();
        private SensorsRunner _sensorsRunner;
        private BindingList<TransferModel> _transferModelList = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnergyCityModel model = new EnergyCityModel();
            SensorsRunner sensorsRunner = new SensorsRunner();
            model.LoadTransfers($"district_help.csv");
            model.ApplyTransfers();
            sensorsRunner.EnergyModel = model;

            sensorsRunner.LoadSensors($"sensors.csv");
            _sensorsRunner = sensorsRunner;
            dgRecomendationsList.ItemsSource = _recomendationsList;
            foreach(TransferModel transferModel in sensorsRunner.EnergyModel.GetTransferModels())
            {
                _transferModelList.Add(transferModel);
            }
            dgTransfersList.ItemsSource = _transferModelList;

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(5);
            dt.Tick += dtTick;
            dt.Start();
        }

        private void dtTick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void Step_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }
        private void UpdateUI()
        {
            _recomendationsList.Clear();
            foreach (RecomendationModel recomendation in _sensorsRunner.GetSuggestions())
            {
                _recomendationsList.Add(recomendation);
            }
            TimeLabel.Text = _sensorsRunner.Date.ToString();
        }
    }
}
