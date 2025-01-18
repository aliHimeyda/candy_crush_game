using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
// Ali HIMEYDA B231200561
namespace oyunum
{
    // Ali HIMEYDA B231200561
    internal static class Program
    {
        public static OyunFormu AnaForm;
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AnaForm = new OyunFormu();
            Application.Run(AnaForm);
           

        }
    }
}
