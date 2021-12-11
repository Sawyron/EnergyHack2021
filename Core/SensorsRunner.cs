using EnergyHack2021.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace EnergyHack2021.Core
{
    public class SensorsRunner
    {
        
        private List<string> _steps;
        private DateTime _date = DateTime.Now;

        public DateTime Date => _date;
        public EnergyCityModel EnergyModel { get; set; }

       
        public void LoadSensors(string fileName)
        {
            _steps = new List<string>(File.ReadAllLines(fileName));
        }
        public List<RecomendationModel> GetSuggestions()
        {
            var recomendations = new List<RecomendationModel>();
            if (!_steps.Any())
            {
                return new List<RecomendationModel>();
            }
            string[] strarr = _steps.First().Split(";");

            DateTime data = DateTime.Parse(strarr[0]);
            TimeSpan time = TimeSpan.Parse(strarr[1]);
            _date = data + time;
            _steps.RemoveAt(0);
            string commandIn = "";
            for (int i = 0; i < EnergyModel.DistrictPowers.Length; i++)
            {
                double distLoad = Convert.ToDouble(strarr[i + 2]);
                double perimLoad = EnergyModel.Perimets[i] * 0.5;
                double d = EnergyModel.DistrictPowers[i] - distLoad - EnergyModel.Perimets[i] * 0.5;
                double neightD = 0;
                for (int j = 0; j < EnergyModel.DistrictPowers.Length; j++)
                {
                    distLoad = Convert.ToDouble(strarr[j + 2]);
                    if (EnergyModel.DistrictNeight[i,j] != 0)
                    {
                        neightD += EnergyModel.DistrictPowers[j] - distLoad - EnergyModel.Perimets[j] * 0.5;
                    }
                }
                commandIn += Convert.ToInt32(d) + " " + Convert.ToInt32(neightD) + " " + Convert.ToInt32(perimLoad) + " ";
            }
            Process Tets = new Process();
            Tets.StartInfo.FileName = @"Tets.exe";
            Tets.StartInfo.Arguments = String.Format("{0}", commandIn);
            Tets.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Tets.Start();
            Tets.WaitForExit(20000);
            string[] outarr = File.ReadAllLines("out.txt");
            for (int i = 0; i < outarr.Length; i++)
            {
                int recomendationCode = Convert.ToInt32(outarr[i]);
                recomendations.Add(new RecomendationModel(i, recomendationCode));
            }
            return recomendations;
        }
    }
}
