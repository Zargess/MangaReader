using MangaReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.ViewModels {
    public class ViewModel {
        public ObservableCollection<Manga> Items { get; set; }

        public ViewModel() {
            Items = new ObservableCollection<Manga>();
            Items.Add(new Manga("The Breaker", "Test", "http://s4.mangareader.net/cover/the-breaker/the-breaker-l0.jpg", new List<string>(), 1));
        }
    }
}
