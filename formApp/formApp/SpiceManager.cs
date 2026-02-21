using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace formApp
{
    public sealed class SpiceManager
    {
        private static SpiceManager instance;

        private List<string> _spiceOptions;
        private Dictionary<string,(string,int,SpiceState)> _spiceState;
        private Dictionary<SpiceState,ListBox> _listBoxes;

        private const int MAX_INDEX = 8;
        private const bool UPDATE_CSVS = false;

        public enum Commands
        {
            Request,
            Return
        }

        public enum SpiceState
        {
            Default,
            Stored,
            Lending,
            Lent,
            Storing,
            Removing,
        }

        public static string State2String(SpiceState state)
        {
            switch (state)
            {
                case SpiceState.Stored:   return "Stored";
                case SpiceState.Lending:  return "Lending";
                case SpiceState.Lent:     return "Lent";
                case SpiceState.Storing:  return "Storing";
                case SpiceState.Removing: return "Removing";
                default: return "Default";
            }
        }

        public static SpiceState String2State(string state)
        {
            switch (state)
            {
                case "Stored":   return SpiceState.Stored;
                case "Lending":  return SpiceState.Lending;
                case "Lent":     return SpiceState.Lent;
                case "Storing":  return SpiceState.Storing;
                case "Removing": return SpiceState.Removing;
                default: return SpiceState.Default;
            }
        }

        private SpiceManager() 
        {
            _spiceOptions = new List<string>();
            _spiceState = new Dictionary<string,(string,int,SpiceState)>();
            _listBoxes = new Dictionary<SpiceState, ListBox>();
            
            LoadSpiceOptions();
            LoadSpiceState();
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

        private void LoadSpiceOptions()
        {
            using(StreamReader reader = new StreamReader("spiceOptions.csv"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                
                    _spiceOptions.Add(line);
                }
            }
        }

        private void LoadSpiceState()
        {
            using (StreamReader reader = new StreamReader("saveState.csv"))
            {
                string header = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    _spiceState.Add(
                        values[0],
                        (values[0], int.Parse(values[1]), String2State(values[2]))
                    );
                }
            }
        }

        public void SaveSpiceOptions()
        {
            using (StreamWriter writer = new StreamWriter("spiceDictionary.csv"))
            {
                foreach (string spice in _spiceOptions)
                {
                    writer.WriteLine(spice);
                }
            }
        }

        public void SaveSpiceState()
        {
            using (StreamWriter writer = new StreamWriter("spiceDictionary.csv"))
            {
                writer.WriteLine("Name,Index,State"); // header

                foreach (KeyValuePair<string,(string,int,SpiceState)> entry in _spiceState)
                {
                    (string name, int index, SpiceState state) = entry.Value;
                    writer.WriteLine($"{name},{index},{State2String(state)}");
                }
            }
        }

        public List<string> Options
        {
            get { return new List<string>(_spiceOptions); }
        }

        public Dictionary<string,(string,int,SpiceState)> State
        {
            get { return new Dictionary<string,(string,int,SpiceState)>(_spiceState); }
        }

        private int GetNextEmptyIndex()
        {
            HashSet<int> filledIndicies = new HashSet<int>();

            foreach (KeyValuePair<string,(string,int,SpiceState)> entry in _spiceState)
            {
                (_, int index, _) = entry.Value;
                filledIndicies.Add(index);
            }

            for (int i = 0; i < MAX_INDEX; i++)
            {
                if (!filledIndicies.Contains(i)) return i;
            }
            return -1;
        }

        public void UpdateListBox(SpiceState state, ListBox listBox)
        {
            _listBoxes[state] = listBox;
        }

        public int UpdateState(string spice, SpiceState newState)
        {
            if (_spiceState.ContainsKey(spice))
            {
                (_, int index, SpiceState oldState) = _spiceState[spice];
                _spiceState[spice] = (spice,index,newState);

                if (_listBoxes.ContainsKey(oldState))
                {
                    int match = _listBoxes[oldState].FindStringExact(spice);
                    if (match != ListBox.NoMatches)
                    {
                        _listBoxes[oldState].Items.RemoveAt(match);
                    }
                }

                if (_listBoxes.ContainsKey(newState))
                {
                    int match = _listBoxes[newState].FindStringExact(spice);
                    if (match == ListBox.NoMatches)
                    {
                        _listBoxes[newState].Items.Add(spice);
                    }
                }

                return index;
            }
            return -1;
        }

        public int AddSpice(string spice, int index = -1, SpiceState state = SpiceState.Storing)
        {
            if (!_spiceState.ContainsKey(spice))
            {
                if (index == -1) { index = GetNextEmptyIndex(); }
                if (index == -1) { return -1; } // if still -1 no more indexes are available

                _spiceState.Add(spice,(spice,index,state));

                if (UPDATE_CSVS) { SaveSpiceState(); }
                return index;
            }
            return -1;
        }

        public bool AddOption(string spice)
        {
            if (!_spiceOptions.Contains(spice))
            {
                _spiceOptions.Add(spice);
                _spiceOptions.Sort();
                
                if (UPDATE_CSVS) { SaveSpiceOptions(); }
                return true;
            }
            return false;
        }

        public bool RemoveSpice(string spice)
        {
            if (_spiceState.ContainsKey(spice))
            {
                _spiceState.Remove(spice);

                if (UPDATE_CSVS) { SaveSpiceState(); }
                return false;
            }
            return true;
        }

        public Grammar BuildGrammer()
        {
            Choices choices = new Choices(_spiceOptions.ToArray());
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(choices);

            return new Grammar(gb);
        }

    }
}
