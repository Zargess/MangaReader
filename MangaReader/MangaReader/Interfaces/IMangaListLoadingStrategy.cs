using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Interfaces {
    public interface IMangaListLoadingStrategy {
        Task<List<IManga>> LoadListAsync(string path);
    }
}
