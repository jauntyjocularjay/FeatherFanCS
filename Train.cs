using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace DMBTools
{
    /// <summary>
    /// A Train is a sequence of booleans. Think of a 
    /// "train" of linked states that run along a track.
    /// In this case, this is taken from a sequence of
    /// peacock feathers which are fanned out in a 
    /// display.
    /// 
    /// Each boolean can be thought of as a "car" or 
    /// "feather" in the train, indicating an on/off or 
    /// flag state.
    /// </summary>
    /// 
    /// <remarks>
    /// Planned:
    ///  - TODO XOr() method for exclusive-or logic
    ///  - TODO XAnd() method
    /// </remarks>
    public class Train
    {
        private Dictionary<string, bool> _feathers = new Dictionary<string, bool>();
        public void Set(string name, bool state) => _feathers[name] = state;
        public bool Get(string name) => _feathers.TryGetValue(name, out var state) && state;
        public IEnumerable<string> Names => _feathers.Keys;
        public IEnumerable<KeyValuePair<string, bool>> All => _feathers;

        public bool And()
        {
            CheckIfEmpty();

            foreach (KeyValuePair<string, bool> pair in _feathers)
            {
                if (!pair.Value)
                {
                    return false;
                }
            }
            return true;
        }
        public bool And(Train subset)
        {
            return subset.And();
        }

        public bool Or()
        {
            CheckIfEmpty();

            foreach (KeyValuePair<string, bool> pair in _feathers)
            {
                if (!pair.Value)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Or(Train subset)
        {
            return subset.Or();
        }
        Train Subset(string[] names)
        {
            CheckIfAllIndicesArePresent(names);

            Train subset = new Train();
            foreach (string name in names)
            {
                subset._feathers.Add(name, _feathers[name]);
            }
            return subset;
        }

        public Dictionary<string, bool> ToDictionary()
        {
            return new Dictionary<string, bool>(_feathers);
        }
        void CheckIfEmpty()
        {
            if (_feathers.Count == 0) throw new InvalidOperationException("Cannot evaluate an empty train.");
        }
        void CheckIfAllIndicesArePresent(string[] indices)
        {
            Dictionary<string, bool>.KeyCollection keys = _feathers.Keys;
            List<string> notfound = new List<string>();

            foreach (string index in indices)
            {
                if (!keys.Contains(index)) notfound.Add(index);
            }

            if (notfound.Count > 0) throw new InvalidDataException($"The indices: {notfound} were not found in this train.");
        }
    }
}
