using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Interfaces {
    public interface IManga {
        string Title { get; set; }
        string Description { get; set; }
        string ImagePath { get; set; }
        IEnumerable<string> Chapters { get; set; }
        int LastChapterRead { get; set; }

        void Load();
    }
}
