using System.Data;

namespace DataGridViewImages.Classes
{
    internal class Operations
    {
        public static Dictionary<int, byte[]> SmallImages()
        {
            Dictionary<int, byte[]> dictionary = new Dictionary<int, byte[]>
            {
                { 1, File.ReadAllBytes("blueInformation_16.png") },
                { 2, File.ReadAllBytes("radiobutton16.png") }
            };

            return dictionary;
        }

        public static DataTable Table()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("image", typeof(byte[]));
            dt.Columns.Add("text", typeof(string));

            var images = SmallImages();

            dt.Rows.Add(images[1], "Some text");
            dt.Rows.Add(images[2], "More text");

            return dt;
        }
    }
}
