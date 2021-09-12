using Demo01_09.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01_09.Io
{
    public interface INewsRepository
    {
        List<Publisher> GetNews();
        void Save(List<Publisher> list);
    }
}
