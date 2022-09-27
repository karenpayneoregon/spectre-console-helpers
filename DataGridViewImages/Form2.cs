using DataGridViewImages.Classes;

namespace DataGridViewImages
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Shown += OnShown;
            dataGridView1.SelectionChanged += DataGridView1OnSelectionChanged;
            dataGridView1.RowHeadersVisible = false;
        }

        private void DataGridView1OnSelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex]
                    .Cells[0].Selected = false;
            }
        }

        private void OnShown(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Operations.Table();
            dataGridView1.Columns[0].HeaderText = "";
            dataGridView1.Columns[0].Width = 25;
        }
    }
}
