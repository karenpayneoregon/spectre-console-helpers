namespace DataGridViewImages.Classes;

internal class Utilities
{
    public static Image ByteArrayToImage(byte[] bytes)
    {
        var converter = new ImageConverter();
        return ((Image)converter.ConvertFrom(bytes))!;
    }

    // for image column
    public static Image ByteArrayToImage1(byte[] contents)
    {
        using var ms = new MemoryStream(contents);
        return Image.FromStream(ms);
    }
}