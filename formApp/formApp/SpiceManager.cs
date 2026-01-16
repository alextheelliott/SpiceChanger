using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formApp
{

    public sealed class SpiceManager
    {
        private static SpiceManager instance;

        private Dictionary<string,int> _spicesStored;
        private Dictionary<string,int> _spicesRequesting;
        private Dictionary<string,int> _spicesLent;
        private Dictionary<string,int> _spicesReturning;

        private SpiceManager() 
        {
            _spicesStored = new Dictionary<string,int>();
            _spicesRequesting = new Dictionary<string,int>();
            _spicesLent = new Dictionary<string,int>();
            _spicesReturning = new Dictionary<string,int>();

            LoadSpiceDict();
        }

        //Singleton getter
        public static SpiceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpiceManager();
                }
                return instance;
            }
        }

        private void LoadSpiceDict()
        {
            using(StreamReader reader = new StreamReader("spiceDictionary.csv"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    _spicesStored.Add(values[0],int.Parse(values[1]));
                }
            }
        }

        private void SaveSpiceDict()
        {
            using(StreamWriter writer = new StreamWriter("spiceDictionary.csv"))
            {
                foreach (KeyValuePair<string, int> entry in _spicesStored)
                {
                    writer.WriteLine($"{entry.Key},{entry.Value}");
                }
            }
        }

        public Dictionary<string,int> SpicesStored
        {
            get { return new Dictionary<string,int>(_spicesStored); }
        }

        public Dictionary<string,int> SpicesLent
        {
            get { return new Dictionary<string,int>(_spicesLent); }
        }

        public int RequestSpice(string RequestedSpice)
        {
            if (_spicesStored.ContainsKey(RequestedSpice))
            {
                _spicesRequesting.Add(RequestedSpice,_spicesStored[RequestedSpice]);
                _spicesStored.Remove(RequestedSpice);

                return _spicesRequesting[RequestedSpice];
            }
            return -1;
        }

        public void ConfirmRequestSpice(string RequestedSpice)
        {
            if (_spicesRequesting.ContainsKey(RequestedSpice))
            {
                _spicesLent.Add(RequestedSpice,_spicesRequesting[RequestedSpice]);
                _spicesRequesting.Remove(RequestedSpice);
            }
        }

        public int ReturnSpice(string LentSpice)
        {
            if (_spicesLent.ContainsKey(LentSpice))
            {
                _spicesReturning.Add(LentSpice, _spicesLent[LentSpice]);
                _spicesLent.Remove(LentSpice);

                return _spicesReturning[LentSpice];
            }
            return -1;
        }

        public void ConfirmReturnSpice(string LentSpice)
        {
            if (_spicesReturning.ContainsKey(LentSpice))
            {
                _spicesStored.Add(LentSpice,_spicesReturning[LentSpice]);
                _spicesReturning.Remove(LentSpice);
            }
        }

    }
}
