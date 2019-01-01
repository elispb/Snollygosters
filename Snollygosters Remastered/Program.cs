using System;

namespace Snollygosters
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Snollygosters game = new Snollygosters())
            {
                game.Run();
            }
        }
    }
#endif
}

