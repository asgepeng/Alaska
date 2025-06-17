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

        private async void HandleFormLoad(object sender, EventArgs e)
        {
            using (var stream = await userService.GetDataDataTable())
            {
                using (var tableBulder = new UserTableBuilder(stream))
                {
                    var table = tableBulder.ToDataTable();
                    this.dataGridView1.DataSource = table;
                }
            }
        }
    }
}
