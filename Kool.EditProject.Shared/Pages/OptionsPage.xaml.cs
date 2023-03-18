using Kool.EditProject.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Kool.EditProject.Pages;

internal sealed partial class OptionsPage : UserControl
{
    private readonly Options _options;

    public OptionsPage(Options options)
    {
        InitializeComponent();
        DataContext = _options = options;
    }

    private void OnBrowseButtonClicked(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog();
        if (!string.IsNullOrWhiteSpace(_options.EditorExe))
        {
            var dir = Path.GetDirectoryName(_options.EditorExe);
            if (Directory.Exists(dir))
            {
                dialog.InitialDirectory = dir;
            }
        }
        if (dialog.ShowDialog() is true)
        {
            EditorExe.Text = dialog.FileName;
        }
    }

    private void OnTestButtonClicked(object sender, RoutedEventArgs e)
    {
        try
        {
            var file = Path.GetTempFileName();
            File.WriteAllText(file, "Hello World");
            new CustomEditor().OpenFile(file);
        }
        catch (Exception ex)
        {
            Models.MessageBox.Error(I18n.ErrorMessageTitle, ex.Message);
        }
    }
}