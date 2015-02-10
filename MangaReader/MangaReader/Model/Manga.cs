using MangaReader.Interfaces;
using MangaReader.Threads;
using MangaReader.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MangaReader.Model {
    public class Manga : IManga, INotifyPropertyChanged {
        private string _url;
        private MangaLoadingThread _thread;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }

        private string _description;
        public string Description {
            get {
                return _description;
            }
            set {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private string _imagepath;
        public string ImagePath {
            get {
                return _imagepath;
            }
            set {
                _imagepath = value;
                RaisePropertyChanged("ImagePath");
            }
        }

        private IEnumerable<string> _chapters;
        public IEnumerable<string> Chapters {
            get {
                return _chapters;
            }
            set {
                _chapters = value;
                RaisePropertyChanged("Chapters");
            }
        }

        private int _lastreadchapter;
        public int LastChapterRead {
            get {
                return _lastreadchapter;
            }
            set {
                _lastreadchapter = value;
                RaisePropertyChanged("LastReadChapter");
            }
        }

        public Manga(string title, string path) {
            Title = title;
            _url = path;
            _thread = new MangaLoadingThread(_url);
            Chapters = new List<string>();
        }

        public override string ToString() {
            var res = "{";
            res += Title + ";";
            res += Description + ";";
            res += LastChapterRead + ";";
            res += ImagePath + ";[";

            foreach (var link in Chapters) {
                res += link + " ";
            }

            res += "]}";

            return res;
        }

        public async void Load() {
            if (!GeneralFunctions.IsConnectedToInternet()) return;
            var info = await _thread.Load();
            Description = info[0];
            ImagePath = info[1];
            var chapters = info[2].Split(';').ToList();
            Chapters = chapters;
        }

        private void RaisePropertyChanged(string name) {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public override bool Equals(object obj) {
            if (obj == this) return true;
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;
            var other = obj as Manga;
            if (other == null) return false;
            return _url == other._url;
        }
    }
}