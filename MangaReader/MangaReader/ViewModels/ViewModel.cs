using MangaReader.Interfaces;
using MangaReader.Model;
using MangaReader.Strategies.Loading.IMangas;
using MangaReader.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MangaReader.ViewModels {
    public class ViewModel : INotifyPropertyChanged {
        public ObservableCollection<IManga> Items { get; set; }
        public ObservableCollection<IManga> AllMangas { get; set; }
        public ObservableCollection<IManga> SearchedMangas { get; set; }
        public ObservableCollection<string> Letters { get; set; }

        public ViewModel() {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Items = new ObservableCollection<IManga>();
            AllMangas = new ObservableCollection<IManga>();
            SearchedMangas = new ObservableCollection<IManga>();
            // TODO : Load kun hvis man har netværks forbindelse
            watch.Stop();
            Debug.WriteLine("Loading time: " + watch.ElapsedMilliseconds);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void InitAsync() {
            if (GeneralFunctions.IsConnectedToInternet()) LoadMangaListAsync();
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html", new MangareaderMangaLoadingStrategy()));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html", new MangareaderMangaLoadingStrategy()));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html", new MangareaderMangaLoadingStrategy()));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html", new MangareaderMangaLoadingStrategy()));
        }

        public async void LoadMangaListAsync() {
            AllMangas.Clear();
            SearchedMangas.Clear();
            if (AllMangas.Count > 0) return;
            var list = await GeneralFunctions.GenerateMangaListAsync();
            foreach (var manga in list) {
                AllMangas.Add(manga);
                SearchedMangas.Add(manga);
            }
        }

        public void Search(string queuery) {
            if (String.IsNullOrEmpty(queuery) || String.IsNullOrWhiteSpace(queuery)) {
                AddMangasToSearchedCollection(AllMangas);
            } else {
                var collection = AllMangas.Where(x => x.Title.ToLower().Contains(queuery.ToLower()));
                AddMangasToSearchedCollection(collection);
            }
        }

        private void AddMangasToSearchedCollection(IEnumerable<IManga> collection) {
            SearchedMangas.Clear();
            var counter = 0;
            foreach (var manga in collection) {
                if (counter < 20) manga.Load();
                SearchedMangas.Add(manga);
                counter++;
            }
        }
    }
}
