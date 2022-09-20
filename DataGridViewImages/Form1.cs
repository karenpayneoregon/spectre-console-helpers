using DataGridViewImages.Data;
using DataGridViewImages.Models;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using static DataGridViewImages.Classes.Utilities;

namespace DataGridViewImages
{
    public partial class Form1 : Form
    {
        private readonly BindingSource _bindingSource = new();

        public Form1()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            Shown += OnShown;
        }

        /// <summary>
        /// Load DataGridView with Categories
        /// For a real app we would have a data class
        /// </summary>
        private async void OnShown(object sender, EventArgs e)
        {
            var categoriesList = new List<Categories>();

            await Task.Run(async () =>
            {
                await using var context = new NorthWindContext();
                categoriesList = await context.Categories.ToListAsync();
            });

            _bindingSource.DataSource = categoriesList;
            dataGridView1.DataSource = _bindingSource;

            DiscriptionLabel.DataBindings.Add(
                "Text", 
                _bindingSource, 
                nameof(Categories.Description));
        }

        private void CurrentButton_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current is null) return;
            var category = (Categories)_bindingSource.Current;

            var image = ByteArrayToImage(category.Picture);
            image.Save($"{category.CategoryName}.png", ImageFormat.Png);
        }
        
        private void AddCategoryButton_Click(object sender, EventArgs e)
        {
            
            using var context = new NorthWindContext();
            Categories categories = new()
            {
                CategoryName = "Tools",
                Description = "Tool box items",
                Picture = File.ReadAllBytes("toolbox.png")
            };

            ((List<Categories>)_bindingSource.DataSource).Add(categories);

            _bindingSource.ResetBindings(false);
            
        }
    }
}