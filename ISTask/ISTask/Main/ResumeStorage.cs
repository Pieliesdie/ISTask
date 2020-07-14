using ISTask.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ISTask.Main
{
    public class ResumeStorage : IResumeStorage
    {
        string path;
        List<ResumeModel> resumes;
        XmlSerializer formatter = new XmlSerializer(typeof(List<ResumeModel>));

        public ResumeStorage(string pathToStorage)
        {
            path = pathToStorage;
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                resumes = (List<ResumeModel>)formatter.Deserialize(fs);
            }
        }

        public List<ResumeModel> Resumes => new List<ResumeModel>(resumes);

        public bool Add(ResumeModel resume)
        {
            try
            {
                resumes.Add(resume);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(ResumeModel resume)
        {
            try
            {
                resumes.Remove(resume);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual ResumeModel Find(string login)
        {
            return resumes.Where(x => x?.Login == login).FirstOrDefault();
        }

        public void Save()
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, resumes);
            }
        }

        public bool Update(ResumeModel resume)
        {
            try
            {
                var oldresume = resumes.Find(x => x?.Login == resume.Login);
                resumes.Remove(oldresume);
                resumes.Add(resume);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
