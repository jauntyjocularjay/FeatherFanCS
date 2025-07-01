using System;
using System.Collections.Generic;
using System.Linq;

namespace DMBTools
{
    public partial class Fan
    {
        public List<Feather> _feathers = new List<Feather>();

        /*** Constructors ***/
        public Fan(string[] keys)
        {
            foreach (string key in keys)
            {
                Add(key, false);
            }

        }
        public Fan(){}

        /*** Getters and Setters ***/
        public bool Get(string name)
        {
            Feather match = _feathers.FirstOrDefault(feather => feather.key == name);

            if (match != null) return match.value;

            else throw new ArgumentException($"There is no entry for {name}");
        }
        public void Set(string name, bool value)
        {
            Feather match = _feathers.FirstOrDefault<Feather>(feather => feather.key == name);

            if (match != null)
                match.value = value;
            else
                Add(match.key, match.value);
        }

        /*** Enumeration ***/
        public IEnumerable<Feather> All => _feathers;

        /*** Adders ***/
        public void Add(Feather feather)
        {
            _feathers.Add(feather);
        }
        public void Add(string key, bool value)
        {
            _feathers.Add(new Feather(key, value));
        }
        public void Add(string key)
        {
            Add(new Feather(key, false));
        }

        /*** Logic Gates ***/
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
        public bool AND() => And();

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
        public bool OR() => Or();

        // @TODO Finish the NAND gate
        public bool NAnd() // NAND: NOT And
        {
            CheckIfEmpty();
            // ...
            return false;
        }
        public bool NAND() => NAnd();

        public bool NOr() // NOR: Not Or
        {
            CheckIfEmpty();
            // ...
            return false;
        }
        public bool NOR() => NOr();

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

        public bool XOr()
        {
            if (Count() < 2) throw new InvalidOperationException
            (
                $"XOr() requires at least 2 booleans in the Fan."
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
        public bool XOR() => XOr();

        public bool XNor(bool vacuous = false)
        {
            bool control;

            if (vacuous)
            {
                return VacuouslyTrue();
            }
            else
            {
                CheckIfEmpty();
            }

            control = _feathers[0].value;

            for (int i = 1; i < _feathers.Count; i++)
            {
                if (_feathers[i].value != control) return false;
            }

            return true;
        }
        public bool XNOR(bool vacuous = false) => XNor(vacuous);


        public bool Imply()
        {
            throw new NotImplementedException();
        }
        public bool IMPLY() => Imply();

        public bool NImply()
        {
            throw new NotImplementedException();
        }
        public bool NIMPLY() => NImply();

        /*** Indexer ***/
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

        /*** Helper Functions ***/
        public bool VacuouslyTrue()
        {
            if (_feathers.Count == 0) return true;
            else return false;
        }

        public bool VacuouslyFalse()
        {
            if (_feathers.Count == 0)
                return false;
            else
                return true;
        }

        public Feather Find(string key)
        {
            for (int i = 0; i < _feathers.Count; i++)
            {
                if (_feathers[i].key == key) return _feathers[i];
            }

            throw new KeyNotPresentException(key);
        }

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

        public int Count()
        {
            return _feathers.Count();
        }

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

        public Dictionary<string, bool> ToDictionary()
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();

            foreach (Feather feather in _feathers)
            {
                result.Add(feather.key, feather.value);
            }

            return result;
        }

        public List<string> Keys()
        {
            List<string> result = new List<string>{};

            foreach (Feather feather in _feathers)
            {
                result.Add(feather.key);
            }

            return result;
        }
        public List<bool> Values()
        {
            List<bool> result = new List<bool> { };

            foreach (Feather feather in _feathers)
            {
                result.Add(feather.value);
            }

            return result;
        }

        /*** Verifiers ***/
        void CheckIfEmpty()
        {
            if (_feathers.Count == 0) throw new InvalidOperationException("Cannot evaluate an empty train.");
        }
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
        bool KeyIsPresent(string key)
        {
            foreach (Feather f in _feathers)
            {
                if (key == f.key) return true;
            }

            return false;
        }

        /*** Exceptions ***/
        public class KeyNotPresentException : KeyNotFoundException
        {
            public KeyNotPresentException() { }
            public KeyNotPresentException(string key) : base($"{key} is not present in the Fan.") { }
            public KeyNotPresentException(List<string> keys) : base($"The keys {keys} are not present in this Fan.") { }
        }

    }
}
