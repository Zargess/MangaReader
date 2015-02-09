using MangaReader.Interfaces;
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
        public ObservableCollection<IManga> Items { get; set; }
        public ObservableCollection<IManga> AllMangas { get; set; }

        public ViewModel() {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Items = new ObservableCollection<IManga>();
            AllMangas = new ObservableCollection<IManga>();
            //LoadMangaList();
            //Task.Run(() => LoadSubscribedMangas());
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            watch.Stop();
            Debug.WriteLine(watch.ElapsedMilliseconds);
        }

        private async void LoadSubscribedMangas() {
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker", "http://www.mangareader.net/530/the-breaker.html"));
            Items.Add(new Manga("The Breaker: New Waves", "http://www.mangareader.net/the-breaker-new-waves"));
        }

        public async void LoadMangaList() {
            if (AllMangas.Count > 0) return;
            var list = await GeneralFunctions.GenerateMangaListAndSaveResult();
            foreach (var manga in list) {
                AllMangas.Add(manga);
            }
        }
    }
}
