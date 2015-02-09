using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Interfaces {
    public interface IManga {
        string Title { get; }
        string Description { get; }
        string ImagePath { get; set; }
        IEnumerable<string> Chapters { get; }
        int LastChapterRead { get; }

        void Load();
    }
}
