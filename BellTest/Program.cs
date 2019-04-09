using System;
using System.Windows.Forms;

namespace BellTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Instrument instrument = new Instrument();
            instrument.Closed += InstrumentClosed;
            Application.Run(instrument);
        }

        static void InstrumentClosed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
