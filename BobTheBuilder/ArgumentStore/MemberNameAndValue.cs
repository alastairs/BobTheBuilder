namespace BobTheBuilder.ArgumentStore
{
    public class MemberNameAndValue
    {
        private readonly string name;
        private readonly object value;

        internal MemberNameAndValue(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name { get { return name; } }
        public object Value { get { return value; } }
    }
}