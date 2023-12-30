using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class MZNoteViewModel : ObservableObject, IQueryAttributable
{
    private Models.MZNote _note;

    public string Text
    {
        get => _note.mzText;
        set
        {
            if (_note.mzText != value)
            {
                _note.mzText = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime Date => _note.mzDate;

    public string Identifier => _note.mzFilename;

    public ICommand SaveCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }

    public MZNoteViewModel()
    {
        _note = new Models.MZNote();
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    public MZNoteViewModel(Models.MZNote note)
    {
        _note = note;
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    private async Task Save()
    {
        _note.mzDate = DateTime.Now;
        _note.Save();
        await Shell.Current.GoToAsync($"..?saved={_note.mzFilename}");
    }

    private async Task Delete()
    {
        _note.Delete();
        await Shell.Current.GoToAsync($"..?deleted={_note.mzFilename}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _note = Models.MZNote.Load(query["load"].ToString());
            RefreshProperties();
        }
    }

    public void Reload()
    {
        _note = Models.MZNote.Load(_note.mzFilename);
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Text));
        OnPropertyChanged(nameof(Date));
    }
}


