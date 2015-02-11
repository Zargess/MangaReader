using HtmlAgilityPack;
using MangaReader.Interfaces;
using MangaReader.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace MangaReader.Threads {
    public class MangaLoadingThread {
        private string _path;

        public MangaLoadingThread(string path) {
            _path = path;
        }

        public async Task<string[]> Load() {
            var doc = new HtmlDocument();
            var html = await GeneralFunctions.GetHtmlAsync(_path);
            doc.LoadHtml(html);

            var descriptions = GeneralFunctions.FindNodes(doc, "div", "id", "readmangasum").ToList()[0].Descendants("p").ToList()[0].InnerText;
            var image = GeneralFunctions.FindNodes(doc, "div", "id", "mangaimg").ToList()[0].Descendants().ToList()[0].Attributes["src"].Value;
            var chapters = GeneralFunctions.FindChapters(doc);
            
            return new string[] { descriptions, image, chapters };
        }
    }
}
