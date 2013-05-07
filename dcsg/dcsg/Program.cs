using System;

namespace dcsg
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DCSG game = new DCSG())
            {
                game.Run();
            }
        }
    }
#endif
}

