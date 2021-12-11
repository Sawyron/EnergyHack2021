using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHack2021.MVVM.Model
{
    public class EnergyCityModel
    {
        private struct Transfer
        {
            public int From { get; set; }
            public int To { get; set; }
            public int Amount { get; set; }

            public Transfer(int from, int to, int amount)
            {
                From = from;
                To = to;
                Amount = amount;
            }
        }
        private List<Transfer> _transfers = new();
        private readonly int[,] DISTRICT_HEIGHT = {
                {0, 1, 1, 0, 0, 1, 0, 0, 1},
                {1, 0, 0, 0, 0, 1, 1, 0, 0},
                {1, 0, 0, 1, 0, 0, 0, 0, 1},
                {0, 0, 1, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 1, 0, 0, 1},
                {1, 1, 0, 0, 1, 0, 1, 0, 1},
                {0, 1, 0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0},
                {1, 0, 1, 0, 1, 1, 0, 1, 0}
            };
        private int[] _districtPowers = { 850, 0, 0, 384, 0, 1200, 0, 280, 0 };
        public int[] DistrictPowers
        {
            get { return _districtPowers; }
        }
        public int[,] DistrictNeight => DISTRICT_HEIGHT;
        public double[] Perimets { get; } = { 17.9, 16.2, 29, 12.7, 9.6, 8.6, 13.9, 55.7, 0 };

        public void LoadTransfers(string fileName)
        {
            foreach (string line in File.ReadAllLines(fileName))
            {
                string[] args = line.Split(";");
                int from = Convert.ToInt32(args[2]);
                int to = Convert.ToInt32(args[3]);
                int amount = Convert.ToInt32(args[4]);
                _transfers.Add(new Transfer(from, to, amount));
            }
        }
        public void ApplyTransfers()
        {
            foreach(Transfer transfer in _transfers)
            {
                _districtPowers[transfer.From] -= transfer.Amount;
                _districtPowers[transfer.To] += transfer.Amount;
            }
        }
        public void ApplyTransfer(int from, int to)
        {
            int index = _transfers.FindIndex(t => t.From == from && t.To == to);
            if (index > 0)
            {
                Transfer transfer = _transfers[index];
                _districtPowers[transfer.From] += transfer.Amount;
                _districtPowers[transfer.From] -= transfer.Amount;
            }
        }
        public List<TransferModel> GetTransferModels()
        {
            List<TransferModel> transferModels = new();
            foreach(Transfer transfer in _transfers)
            {
                transferModels.Add(new TransferModel(transfer.From, transfer.To, transfer.Amount));
            }
            return transferModels;
        }
    }
}
