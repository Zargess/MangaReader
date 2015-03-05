using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Interfaces {
    public interface IMangaLoadingStrategy {
        Task LoadAsync(IManga manga, string path);
    }
}
