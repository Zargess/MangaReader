using MangaReader.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MangaReader.Model {
    public class Manga {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public IEnumerable<string> Chapters { get; private set; }
        public int ChapterNumber { get; private set; }
        public string CurrentChapter {
            get {
                IList<string> elements = new List<string>(Chapters);
                return "Chapter " + ChapterNumber + "/" + elements.Count;
            }
        }
        
        public string TotalChapters {
            get {
                return Chapters.ToList().Count + " Chapters";
            }
        }

        public Manga(string title, string description, string image, IEnumerable<string> chapters, int chapterNumber) {
            Title = title;
            Description = description;
            ImagePath = image;
            Chapters = chapters;
            ChapterNumber = chapterNumber;
        }
    }
}