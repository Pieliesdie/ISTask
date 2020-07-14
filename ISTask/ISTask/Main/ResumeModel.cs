using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ISTask.Main
{
    public class ResumeViewModel:ViewModelBase
    {
        private ResumeModel resumeModel;

        public ResumeModel ResumeModel { get => resumeModel; set { resumeModel = value; OnPropertyChanged("ResumeModel"); } }

    }

    public class ResumeModel : ViewModelBase
    {
        private ObservableCollection<string> languages = new ObservableCollection<string>();
        private ObservableCollection<string> features = new ObservableCollection<string>();

        public string Login { get; set; }

        public FullName FullName { get; set; } = new FullName();

        public DateTime Birthday { get; set; }

        public string Location { get; set; }

        public ObservableCollection<string> Languages { get => languages; set { languages = value; OnPropertyChanged("Languages"); } }

        public string Email { get; set; }

        public ObservableCollection<string> Features { get => features; set { features = value; OnPropertyChanged("Features"); } }

        public string About { get; set; }
    }

    public class FullName
    {
        public FullName() { }

        public FullName(string name, string surname, string middlename)
        {
            Name = name;
            Surname = surname;
            Middlename = middlename;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname} {Middlename}";
        }
    }
}
