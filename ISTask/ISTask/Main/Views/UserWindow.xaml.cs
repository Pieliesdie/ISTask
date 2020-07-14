using ISTask.Authentication;
using ISTask.Main;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace ISTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        IResumeStorage resumeStorage;
        public string login { get; set; }
        public List<ResumeModel> Resumes { get; }
        public ResumeViewModel Resume { get; set; } = new ResumeViewModel();
        public bool IsEditable { get; }

        public UserWindow(string Login, Role role)
        {
            InitializeComponent();

            login = Login;
            this.DataContext = this;
            resumeStorage = new ResumeStorage(Properties.Settings.Default.ResumesStorage);
            switch (role)
            {
                case Role.User:
                    Resumes = new List<ResumeModel>() { resumeStorage.Find(Login) };
                    IsEditable = true;
                    break;
                case Role.Admin:
                    Resumes = resumeStorage.Resumes;
                    IsEditable = false;
                    break;
            }
            Resume.ResumeModel = Resumes.FirstOrDefault() ?? new ResumeModel();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new AuthWindow().Show();
            this.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Resume.ResumeModel = (sender as ListBox).SelectedItem as ResumeModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isSaved;

            if (Resume.ResumeModel.Login != null)
            {
                isSaved = resumeStorage.Update(Resume.ResumeModel);
                resumeStorage.Save();
            }
            else
            {
                Resume.ResumeModel.Login = login;
                isSaved = resumeStorage.Add(Resume.ResumeModel);
                resumeStorage.Save();
            }
            if (isSaved)
                new MessageBlock("Resume was saved") { Owner = this }.ShowDialog();
            else
                new MessageBlock("Error") { Owner = this }.ShowDialog();

        }

        private void DialogHostFeatures_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (((bool)eventArgs.Parameter))
            {
                Resume.ResumeModel.Features.Add(FeatureTextBox.Text);
            }
        }

        private void DialogHost_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (((bool)eventArgs.Parameter))
            {
                Resume.ResumeModel.Languages.Add(LanguagesTextBox.Text);
            }
        }

        private void SaveXML_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (!saveFileDialog.ShowDialog() ?? false)
                return;
            XmlSerializer formatter = new XmlSerializer(typeof(ResumeModel));
            using (FileStream fs = new FileStream(saveFileDialog.FileName+".xml", FileMode.Create))
            {
                formatter.Serialize(fs, Resume.ResumeModel);
            }
            new MessageBlock("Saved") { Owner = this }.ShowDialog();
        }

        private void PrintResume_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
