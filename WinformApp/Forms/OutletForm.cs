using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformApp.Data;

namespace WinformApp.Forms
{
    public partial class OutletForm : Form
    {
        private readonly OutletService outletService;
        public OutletForm()
        {
            InitializeComponent();
            outletService = new OutletService();
        }

        private async void FormLoadHandler(object sender, EventArgs e)
        {
            using (var stream = await outletService.GetDataDataTable())
            {
                using (var tableBulder = new OutletTableBuilder(stream))
                {
                    var table = tableBulder.ToDataTable();
                    this.dataGridView1.DataSource = table;
                }
            }
        }
    }
}
