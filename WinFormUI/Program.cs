using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
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
            //Application.Run(new Login());
            Application.Run(new PeopleForm());
            //this.Invoke((MethodInvoker)delegate () {
            //    form.Text = "Controle de Estacionamento - São Rafael Carretas";
            //    form.Show();
            //});
        }
    }
}
