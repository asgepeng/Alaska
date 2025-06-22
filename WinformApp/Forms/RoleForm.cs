using Alaska.Data;
using Alaska.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformApp.Forms
{
    public partial class RoleForm : Form
    {
        public RoleForm()
        {
            InitializeComponent();
        }
        async Task LoadRolesAsync()
        {
            this.listView1.Items.Clear();
            using (RoleTableBuilder builder = new RoleTableBuilder(await HttpClientSingleton.GetStreamAsync("/role-manager")))
            {
                var table = builder.ToDataTable();
                foreach (DataRow row in table.Rows)
                {
                    var lvi = listView1.Items.Add(row["name"].ToString(), 0);
                    lvi.SubItems.Add(row["creator"].ToString());
                    lvi.SubItems.Add(Convert.ToDateTime(row["createdDate"]).ToString("dd-MM-yyyy"));
                    lvi.Tag = Convert.ToInt32(row["id"]);
                }
            }
        }

        private async void HandleFormLoad(object sender, EventArgs e)
        {
            await LoadRolesAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var dialog = new RoleDetailForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                await LoadRolesAsync();
            }
        }

        private async void HandleListViewItemActivated(object sender, EventArgs e)
        {
            var item = this.listView1.SelectedItems[0];
            var id = item.Tag is null ? 0 : (int)item.Tag;
            var dialog = new RoleDetailForm();
            dialog.Tag = id;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                await LoadRolesAsync();
            }
        }

        private async void HandleDeleteButtonClicked(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count <= 0) return;
            var item = this.listView1.SelectedItems[0];
            var id = item.Tag is null ? 0 : (int)item.Tag;
            if (id == 0) return;
            if (MessageBox.Show("Yakin menghapus data role dari database?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var json = await HttpClientSingleton.DeleteAsync("/role-manager/" + id.ToString());
                var success = json.Trim() == "true";
                if (success)
                {
                    await LoadRolesAsync();
                }
            }
        }

        private void HandleListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            this.deleteButton.Enabled = this.listView1.SelectedItems.Count > 0;
        }
    }

    public class RoleTableBuilder : DataTableBuilder
    {
        public RoleTableBuilder(Stream stream) : base(stream) { }
        public override DataTable ToDataTable()
        {
            AddColumn("id", typeof(int));
            AddColumn("name", typeof(string));
            AddColumn("creator", typeof(string));
            AddColumn("createdDate", typeof(DateTime));
            while (Read())
            {
                var values = new object[]
                {
                    ReadInt32(), ReadString(), ReadString(), ReadDateTime()
                };
                AddRow(values);
            }
            return base.ToDataTable();
        }
    }
}
