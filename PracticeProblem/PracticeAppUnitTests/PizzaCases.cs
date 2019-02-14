using System.IO;

namespace PracticeAppUnitTests
{
    public class PizzaCases
    {
        public static TextReader Tiny => new StringReader("3 3 1 3\nTTT\nTMM\nTTT");

        public static TextReader Example => new StringReader("3 5 1 6\nTTTTT\nTMMMT\nTTTTT");

        public static TextReader Small =>
            new StringReader("6 7 1 5\nTMMMTTT\nMMMMTMM\nTTMTTMT\nTMMTMMM\nTTTTTTM\nTTTTTTM");

    }
}
