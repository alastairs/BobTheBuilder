namespace BobTheBuilder.Tests
{
    internal class Tag
    {
        private readonly string name;

        public Tag(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }
    }
}