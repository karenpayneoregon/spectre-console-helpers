#nullable disable
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataGridViewImages.Models;

public partial class Categories : INotifyPropertyChanged
{
    private int _categoryId;
    private string _categoryName;
    private string _description;
    private byte[] _picture;

    public int CategoryId
    {
        get => _categoryId;
        set
        {
            if (value == _categoryId) return;
            _categoryId = value;
            OnPropertyChanged();
        }
    }

    public string CategoryName
    {
        get => _categoryName;
        set
        {
            if (value == _categoryName) return;
            _categoryName = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (value == _description) return;
            _description = value;
            OnPropertyChanged();
        }
    }

    public byte[] Picture
    {
        get => _picture;
        set
        {
            if (Equals(value, _picture)) return;
            _picture = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}