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
        private Dictionary<Index, bool> _feathers = new Dictionary<Index, bool>();
        public void Set(Index name, bool state) => _feathers[name] = state;
        public bool Get(Index name) => _feathers.TryGetValue(name, out var state) && state;
        public IEnumerable<Index> Names => _feathers.Keys;
        public IEnumerable<KeyValuePair<Index, bool>> All => _feathers;

        public bool And()
        {
            CheckIfEmpty();

            foreach (KeyValuePair<Index, bool> pair in _feathers)
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

            foreach(KeyValuePair<Index, bool> pair in _feathers)
            {
                if (pair.Value)
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
        Train Subset(Index[] names)
        {
            CheckIfAllIndicesArePresent(names);

            Train subset = new Train();
            foreach (Index name in names)
            {
                subset._feathers.Add(name, _feathers[name]);
            }
            return subset;
        }

        public Dictionary<Index, bool> ToDictionary()
        {
            return new Dictionary<Index, bool>(_feathers);
        }
        void CheckIfEmpty()
        {
            if (_feathers.Count == 0) throw new InvalidOperationException("Cannot evaluate an empty train.");
        }
        void CheckIfAllIndicesArePresent(Index[] indices)
        {
            Dictionary<Index, bool>.KeyCollection keys = _feathers.Keys;
            List<Index> notfound = new List<Index>();

            foreach (Index index in indices)
            {
                if (!keys.Contains(index)) notfound.Add(index);
            }

            if (notfound.Count > 0) throw new InvalidDataException($"The indices: {notfound} were not found in this train.");
        }

        public readonly struct Index
        {
            readonly string _field;

            public Index(string name)
            {
                _field = name;
            }

            readonly public bool Equals(Index i)
            {
                return _field == i._field;
            }
            readonly public bool Equals(string str)
            {
                return _field == str;
            }
            readonly override public bool Equals(Object obj)
            {
                if ( obj is Index other && _field == other._field )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public readonly override int GetHashCode()
            {
                return _field != null ? _field.GetHashCode() : 0;
            }
        }

    }
}
