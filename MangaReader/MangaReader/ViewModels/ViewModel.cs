using MangaReader.Model;
using MangaReader.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.ViewModels {
    public class ViewModel {
        public ObservableCollection<Manga> Items { get; set; }
        public ObservableCollection<Manga> AllMangas { get; set; }

        public ViewModel() {
            Items = new ObservableCollection<Manga>();
            AllMangas = new ObservableCollection<Manga>();
            Items.Add(new Manga("The Breaker", "Test", "http://s4.mangareader.net/cover/the-breaker/the-breaker-l0.jpg", new List<string>(), 1));
            foreach (var item in Items) {
                Debug.WriteLine(item);
            }
        }
    }
}
