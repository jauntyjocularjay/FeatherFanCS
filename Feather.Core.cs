

namespace DMBTools
{
    /// <summary>
    ///     <see cref="Feather" /> is a class that holds a <see cref="string" />-<see cref="bool" /> pair.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class Feather
    {
        /// <summary>
        ///     key is a unique string identifier for its <see cref="bool" />.
        /// </summary>
        public string key;
        /// <summary>
        ///     value is the <see cref="bool" />.
        /// </summary>
        public bool value;

        /// <summary>
        ///     The standard  <see cref="Feather"/> constructor.
        /// </summary>
        /// <param name="key">Is the <see cref="string" /> identifier</param>
        /// <param name="value">Is the <see cref="bool" /> value</param>
        public Feather(string key, bool value)
        {
            this.key = key;
            this.value = value;
        }

        /// <summary>
        ///     Outputs the value of the  <see cref="Feather"/> as a <see cref="bool" /> and witholds the identifier
        /// </summary>
        /// <returns> a <see cref="bool" /> value of the Feather</returns>
        public override string ToString()
        {
            return value.ToString();
        }

        /// <summary>
        ///     Defines an implicit conversion from <see cref="Feather"/> to <see cref="bool"/>. 
        /// </summary>
        /// <param name="feather"> is a <see cref="Feather"/> to evaluate. </param>
        public static implicit operator bool(Feather feather)
        {
            return feather.value;
        }
    }
}