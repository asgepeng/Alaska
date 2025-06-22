using System.Data;
using WinformApp.Data;

namespace WinformApp.Forms
{
    public partial class UserForm : Form
    {
        private readonly UserService userService;
        public UserForm()
        {
            InitializeComponent();
            userService = new UserService();
        }
        private async Task LoadDataUserAsync()
        {
            var table = await userService.GetDataDataTableAsync();
            foreach (DataRow row in table.Rows)
            {
                var id = Convert.ToInt32(row["id"]);
                var lvi = listView1.Items.Add(row["name"].ToString(), 0);
                lvi.SubItems.Add(row["role"].ToString());
                lvi.SubItems.Add(row["createdBy"].ToString());
                lvi.SubItems.Add(Convert.ToDateTime(row["createdDate"]).ToString("dd-MM-yyyy HH:mm"));
                lvi.Tag = id;
            }
        }
        private async void HandleFormLoad(object sender, EventArgs e)
        {
            await this.LoadDataUserAsync();
        }

        private async void HandleNewButtonClicked(object sender, EventArgs e)
        {
            UserDetailForm dialog = new UserDetailForm(this.userService);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.listView1.Items.Clear();
                await this.LoadDataUserAsync();
            }
        }

        private async void listView1_ItemActivate(object sender, EventArgs e)
        {
            var selectedItem = listView1.SelectedItems[0];
            var id = selectedItem.Tag is null ? 0 : (int)selectedItem.Tag;
            UserDetailForm dialog = new UserDetailForm(this.userService);
            dialog.Tag = id;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.listView1.Items.Clear();
                await this.LoadDataUserAsync();
            }
        }

        private void HandleListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            deleteButton.Enabled = this.listView1.SelectedItems.Count > 0;
        }

        private async void HandleDeletedButtonClicked(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count <= 0) return;
            var item = this.listView1.SelectedItems[0];
            var id = item.Tag is null? 0 : (int)item.Tag;
            if (id == 0) return;
            if (id == 1)
            {
                MessageBox.Show("User administrator tidak bisa dihapus", "Hapus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Yakin hapus data user dari database?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var result = await userService.DeleteAsync(id);
                if (result)
                {
                    await LoadDataUserAsync();
                }
            }
        }
    }
}
