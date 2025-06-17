using Alaska.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using WinformApp.Data;

namespace WinformApp
{
    public partial class Form1 : Form
    {
        private UserService userService;
        public Form1()
        {
            InitializeComponent();
            My.Application.ApiUrl = "http://localhost:5005";
            userService = new UserService();
        }

        private async void OpenLoginDialog(object sender, EventArgs e)
        {
            LoginForm dialog = new LoginForm();
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                string username = My.Application.User != null ? My.Application.User.Name : "-";
                this.Text = "SELAMAT DATANG " + username;
                this.WindowState = FormWindowState.Maximized;

                using (var stream = await userService.GetDataDataTable())
                {
                    using (var tableBulder = new UserTableBuilder(stream))
                    {
                        var table = tableBulder.ToDataTable();
                        this.dataGridView1.DataSource = table;
                    }
                }
            }
            else
            {
                this.Close();
            }
        }
    }
    internal class UserTableBuilder : DataTableBuilder
    {
        internal UserTableBuilder(Stream stream) : base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("name", typeof(string));
            AddColumn("role", typeof(string));
            AddColumn("createdBy", typeof(string));
            AddColumn("createdDate", typeof(DateTime));

            while (Read())
            {
                var values = new object[]
                {
                    ReadInt32(),
                    ReadString(),
                    ReadString(),
                    ReadString(),
                    ReadDateTime()
                };
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
