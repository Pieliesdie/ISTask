using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ISTask.Main
{
    public interface IResumeStorage
    {
        bool Add(ResumeModel resume);

        bool Delete(ResumeModel resume);

        bool Update(ResumeModel resume);

        List<ResumeModel> Resumes { get; }

        ResumeModel Find(string Login);

        void Save();
    }
}
