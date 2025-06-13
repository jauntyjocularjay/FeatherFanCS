using System;
using System.Collections.Generic;
using System.Linq;

namespace DMBTools
{
    /// <summary>
    /// A Train is a sequence of booleans. Think of a "train" of linked states that run along a track.
    /// In this case, this is taken from a sequence of peacock feathers which are fanned out in a display. Each boolean can be thought of as a "car" or "feather" in the train, indicating an on/off or flag state.
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

    public partial class Fan
    {
        /// <summary>
        /// a List of <see cref="Feather"/> holding key-bool pairs.
        /// </summary>
        public List<Feather> _feathers;
        /// <summary>
        /// Modifies or creates a value within the <see cref="Fan" />
        /// </summary>
        /// <param name="name">A unique string to identify the flag.</param>
        /// <param name="value">A <see cref="bool" /> flag.</param>
        public void Set(string name, bool value)
        {
            Feather match = _feathers.FirstOrDefault<Feather>(feather => feather.key == name);

            if (match != null)
                match.value = value;
            else
                Add(match.key, match.value);
        }
        /// <summary>
        /// Getter of the value for the given <see cref="string"/> 
        /// </summary>
        /// <param name="name">The identifier to reference.</param>
        /// <returns>
        /// the <see cref="bool"/> value associated with the given <see cref="string"/>
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        public bool Get(string name)
        {
            Feather match = _feathers.FirstOrDefault(feather => feather.key == name);

            if (match != null) return match.value;

            else throw new ArgumentException($"There is no entry for {name}");
        }

        /// <summary>
        /// Iterates over each <see cref="Feather"/> 
        /// </summary>
        public IEnumerable<Feather> All => _feathers;

        /// <summary>
        /// Adds a feather to the <see cref="Fan"/> .
        /// </summary>
        /// <param name="feather"></param>
        public void Add(Feather feather)
        {
            _feathers.Add(feather);
        }
        /// <summary>
        /// Adds a <see cref="Feather"/> by accepting component values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, bool value)
        {
            _feathers.Add(new Feather(key, value));
        }

        /// <summary>
        ///     is true iff all of its values are true
        /// </summary>
        /// <returns>returns true if all values are true, otherwise returns false</returns>
        public bool And()
        {
            CheckIfEmpty();

            foreach (Feather pair in _feathers)
            {
                if (!pair.value)
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

        // @TODO Finish the NAND gate
        public bool NAnd()
        {
            CheckIfEmpty();
            ...
            return false;
        }
        public bool NAND() => NAnd();

        /// <summary>
        ///     is true if any feather is true.
        /// </summary>
        /// <returns>returns true if one value is true, otherwise returns false</returns>
        /// <exception cref="InvalidOperationException">
        ///     throws `InvalidOperationException` if the Train is empty.
        /// </exception>
        public bool Or()
        {
            CheckIfEmpty();

            foreach (Feather pair in _feathers)
            {
                if (pair.value)
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

            foreach (Feather pair in _feathers)
            {
                if (pair.value == true)
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
        ///     Throws `InvalidOperationException` when there are one or fewer values to evaluate.
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
                if (pair.value == true)
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

        /// <summary>
        /// returns the value associated with a <see cref="Feather"/> with this identifier.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotPresentException"> thrown when the key is not present.</exception>
        public bool this[string key]
        {
            get
            {
                for (int i = 0; i < _feathers.Count; i++)
                {
                    if (_feathers[i].key == key) return _feathers[i].value;
                }

                throw new KeyNotPresentException(key);
            }
        }

        /// <summary>
        /// Returns a feather with the given identifier.
        /// </summary>
        /// <param name="key"> a <see cref="string"/> used to identify the value. </param>
        /// <returns>the <see cref="Feather"/> associated with the key.</returns>
        /// <exception cref="KeyNotPresentException"></exception>
        public Feather Find(string key)
        {
            for (int i = 0; i < _feathers.Count; i++)
            {
                if (_feathers[i].key == key) return _feathers[i];
            }

            throw new KeyNotPresentException(key);
        }

        // @TODO Refactor Subset()
        /// <summary>
        /// Creates a Fan from a subset of <see cref="Feather"/>s.
        /// </summary>
        /// <param name="names"> An array of strings to identify the desired <see cref="Feather"/>s.</param>
        /// <returns></returns>
        public Fan Subset(string[] names)
        {
            AllIndicesArePresent(names);

            Fan subset = new Fan();
            foreach (string name in names)
            {
                subset._feathers.Add(new Feather(name, this[name]));
            }
            return subset;
        }

        /// <summary>
        /// The number of elements of <see cref="_feathers"/>
        /// </summary>
        /// <returns>an <see cref="int"/> with the number of elements contained in the <see cref="Feather"/> </returns>
        public int Count()
        {
            return _feathers.Count();
        }

        /// <summary>
        /// A <see cref="string"/> illustration of the Fan
        /// </summary>
        /// <returns>a <see cref="string"/> illustration of the Fan</returns>
        public override string ToString()
        {
            string str = "{\n";

            foreach (Feather feather in _feathers)
            {
                str += $"    \"{feather.key}\": {feather.value} \n";
            }

            str += "}";

            return str;
        }

        // @todo test ToDictionary()
        /// <summary>
        /// Generates a dictionary representation of the Fan.
        /// </summary>
        /// <returns>a <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/> representation of the vaues of the <see cref="Fan"/></returns>
        public Dictionary<string, bool> ToDictionary()
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();

            foreach (Feather feather in _feathers)
            {
                result.Add(feather.key, feather.value);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> Keys()
        {
            List<string> result = new List<string> { };

            foreach (Feather feather in _feathers)
            {
                result.Add(feather.key);
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<bool> Values()
        {
            List<bool> result = new List<bool> { };

            foreach (Feather feather in _feathers)
            {
                result.Add(feather.value);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        void CheckIfEmpty()
        {
            if (_feathers.Count == 0) throw new InvalidOperationException("Cannot evaluate an empty train.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indices"></param>
        /// <exception cref="KeyNotPresentException"></exception>
        void AllIndicesArePresent(string[] indices)
        {
            List<string> keys = Keys();
            List<string> notfound = new List<string>();

            foreach (string index in indices)
            {
                if (!keys.Contains(index)) notfound.Add(index);
            }

            if (notfound.Count != 0) throw new KeyNotPresentException(notfound);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyIsPresent(string key)
        {
            foreach (Feather f in _feathers)
            {
                if (key == f.key) return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public class KeyNotPresentException : KeyNotFoundException
        {
            /// <summary>
            /// 
            /// </summary>
            public KeyNotPresentException() { }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="key"></param>
            public KeyNotPresentException(string key) : base($"{key} is not present in the Train.") { }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="keys"></param>
            public KeyNotPresentException(List<string> keys) : base($"The keys {keys} are not present in this Train.") { }

        }

    }
}
