using AuthLib;

namespace Sample04
{
    public class InMemoryFailCountStore : IFailCountStore
    {
        public int Fails { get; set; } = 0;
    }
}
