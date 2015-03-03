using MangaReader.Interfaces;
using MangaReader.Model;
using MangaReader.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.ViewModels {
    public class ViewModel : INotifyPropertyChanged {
        public ObservableCollection<IManga> Items { get; set; }
        public ObservableCollection<IManga> AllMangas { get; set; }
        public ObservableCollection<string> Letters { get; set; }

        private string _selectedletter;
        public string SelectedLetter {
            get {
                return _selectedletter;
            }
            set {
                _selectedletter = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedLetter"));
            }
        }


        public ViewModel() {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Items = new ObservableCollection<IManga>();
            AllMangas = new ObservableCollection<IManga>();
            Letters = new ObservableCollection<string> {
                "all"
            };
            // TODO : Load kun hvis man har netværks forbindelse
            if (GeneralFunctions.IsConnectedToInternet()) LoadMangaListAsync();
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            watch.Stop();
            Debug.WriteLine(watch.ElapsedMilliseconds);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void LoadSubscribedMangas() {
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker: New Waves", "http://www.mangareader.net/the-breaker-new-waves"));
        }

        public async void LoadMangaListAsync() {
            if (AllMangas.Count > 0) return;
            var list = await GeneralFunctions.GenerateMangaListAsync();
            foreach (var manga in list) {
                AllMangas.Add(manga);
                if (String.IsNullOrEmpty(manga.Title)) continue;
                var letter = manga.Title[0].ToString();
                if (Letters.Contains(letter)) continue;
                Letters.Add(letter);
            }
        }
    }
}
