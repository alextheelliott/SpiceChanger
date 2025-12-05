using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formApp
{
    public enum SpiceManagerState
    {
        Requesting,
        Returning,
        Restocking
    }

    public sealed class SpiceManager
    {
        private static SpiceManager instance;

        private SpiceManagerState _state;
        private Dictionary<string,int> _spicesStored;
        private Dictionary<string,int> _spicesLent;

        private SpiceManager() 
        {
            _state = SpiceManagerState.Requesting;
            _spicesStored = new Dictionary<string,int>();
            _spicesLent = new Dictionary<string,int>();

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

        public SpiceManagerState State
        {
            get { return _state; }
        }

        public Dictionary<string,int> SpicesStored
        {
            get { return new Dictionary<string,int>(_spicesStored); }
        }

        public Dictionary<string,int> SpicesLent
        {
            get { return new Dictionary<string,int>(_spicesLent); }
        }

        public bool RequestSpice(string RequestedSpice)
        {
            if (_state != SpiceManagerState.Requesting) throw new Exception("Spice Mangaer State Violation");

            if (_spicesStored.ContainsKey(RequestedSpice))
            {
                _spicesLent.Add(RequestedSpice,_spicesStored[RequestedSpice]);
                _spicesStored.Remove(RequestedSpice);

                return true;
            }
            return false;
        }

        public bool ReturnSpice(string LentSpice)
        {
            if (_state != SpiceManagerState.Returning) throw new Exception("Spice Mangaer State Violation");

            if (_spicesLent.ContainsKey(LentSpice))
            {
                _spicesStored.Add(LentSpice, _spicesLent[LentSpice]);
                _spicesLent.Remove(LentSpice);

                return true;
            }
            return false;
        }

        public void StartReturning()
        {
            _state = SpiceManagerState.Returning;
        }

        public void StopReturning()
        {
            _state = SpiceManagerState.Requesting;
        }

        public void StartRestocking()
        {
            _state = SpiceManagerState.Restocking;
        }

        public void StopRestocking()
        {
            _state = SpiceManagerState.Requesting;
            SaveSpiceDict();
        }

    }
}
