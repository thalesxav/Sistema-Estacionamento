using DemoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class Consulta : Form
    {
        public Consulta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            /*var userRepository = new UserRepository("Users");
            Console.WriteLine(" Save into table users ");
            var guid = Guid.NewGuid();
            await userRepository.InsertAsync(new User()
            {
                FirstName = "Test2",
                Id = guid,
                LastName = "LastName2"
            });


            await userRepository.UpdateAsync(new User()
            {
                FirstName = "Test3",
                Id = guid,
                LastName = "LastName3"
            });


            List<User> users = new List<User>();

            for (var i = 0; i < 100000; i++)
            {
                var id = Guid.NewGuid();
                users.Add(new User
                {
                    Id = id,
                    LastName = "aaa",
                    FirstName = "bbb"
                });
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
           
           
            Console.WriteLine($"Inserted {await userRepository.SaveRangeAsync(users)}");

            stopwatch.Stop();
            var elapsed_time = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Elapsed time {elapsed_time} ms");
            Console.ReadLine();*/


            DataTable result = SqliteDataAccess.Consulta(textBox1.Text);
            dataGridView1.DataSource = result;
            dataGridView1.Refresh();
            dataGridView1.EndEdit();
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
