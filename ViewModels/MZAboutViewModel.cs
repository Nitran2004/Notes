using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class MZAboutViewModel
{
    public string mzTitle => AppInfo.Name;
    public string mzVersion => AppInfo.VersionString;
    public string mzMoreInfoUrl => "https://aka.ms/maui";
    public string mzMessage => "This app is written in XAML and C# with .NET MAUI.";
    public ICommand ShowMoreInfoCommand { get; }

    public MZAboutViewModel()
    {
        ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
    }

    async Task ShowMoreInfo() =>
        await Launcher.Default.OpenAsync(mzMoreInfoUrl);
}