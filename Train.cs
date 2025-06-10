using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;



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
    ///  - TODO test NAnd() method
    ///  - TODO test Or() method
    ///  - TODO test And() method
    ///  - TODO test Subset() method
    ///  - DONE test ToString()
    ///  - TODO test ToDictionary
    /// </remarks>
    public class Train
    {
        private Dictionary<string, bool> _feathers = new Dictionary<string, bool>();
        public void Set(string name, bool state) => _feathers[name] = state;
        public bool Get(string name) => _feathers.TryGetValue(name, out var state) && state;
        public IEnumerable<string> Indices => _feathers.Keys;
        public IEnumerable<KeyValuePair<string, bool>> All => _feathers;

        public void Add(string str, bool boolean)
        {
            _feathers.Add(str, boolean);
        }

        /// <summary>
        ///     is true iff all of its values are true
        /// </summary>
        /// <returns>returns true if all values are true, otherwise returns false</returns>
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
        /// <inheritdoc cref="And()" />
        /// <remarks>
        ///     This is an alias for <see cref="And()" /> 
        /// </remarks>
        public bool AND() => And();

        public bool Or()
        {
            CheckIfEmpty();

            foreach (KeyValuePair<string, bool> pair in _feathers)
            {
                if (pair.Value)
                {
                    return true;
                }
            }
            return false;
        }
        /// <inheritdoc cref="Or()" />
        /// <remarks>
        ///     This is an alias for <see cref="Or()" /> 
        /// </remarks>
        public bool OR() => Or();

        /// <summary>
        ///     XOne() - returns true iff exactly one value in the train is true.
        ///     XOne is better known as 'one-hot' or 'exactly one'.
        /// </summary>
        /// <returns>
        ///     `true` iff one value in the train is true, otherwise `false`
        /// </returns>
        public bool XOne()
        {
            int has_true = 0;

            foreach (var pair in _feathers)
            {
                if (pair.Value == true)
                {
                    has_true++;
                }
            }

            return has_true == 1;
        }

        /// <summary>
        ///     XOr - With two inputs, XOR is true iff the inputs differ 
        ///     (one is true, one is false). With multiple inputs, XOR is true iff 
        ///     the number of true inputs is odd.
        /// </summary>
        /// <returns>
        ///     `true` when the number of true inputs is odd. Otherwise, returns `false`.
        ///     For a 'one-hot' or 'exactly-one' test, use XOne()
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Throws this when there are not enough values to meaningfully validate
        /// </exception>
        public bool XOr()
        {
            if (Count() < 2) throw new InvalidOperationException
            (
                $"XOr() requires at least 2 booleans in the Train."
            );

            int has_true = 0;
            foreach (var pair in _feathers)
            {
                if (pair.Value == true)
                {
                    has_true++;
                }
            }

            return has_true % 2 != 0;
        }
        /// <inheritdoc cref="XOr()" />
        /// <remarks>
        ///     This is an alias for <see cref="XOr()" /> 
        /// </remarks>
        public bool XOR() => XOr();

        public Train Subset(string[] names)
        {
            CheckIfAllIndicesArePresent(names);

            Train subset = new Train();
            foreach (string name in names)
            {
                subset._feathers.Add(name, _feathers[name]);
            }
            return subset;
        }

        public int Count()
        {
            return _feathers.Count();
        }
        public override string ToString()
        {
            string str = "{\n";

            foreach (var pair in _feathers)
            {
                str += $"    \"{pair.Key}\": {pair.Value} \n";
            }

            str += "}";

            return str;
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
