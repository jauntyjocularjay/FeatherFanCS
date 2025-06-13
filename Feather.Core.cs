

namespace DMBTools
{
    public partial class Feather
    {

        public string key;
        public bool value;

        public Feather(string key, bool value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static implicit operator bool(Feather f)
        {
            return f.value;
        }
    }
}