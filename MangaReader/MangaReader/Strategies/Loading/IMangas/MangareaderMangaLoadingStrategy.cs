using HtmlAgilityPack;
using MangaReader.Interfaces;
using MangaReader.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Strategies.Loading.IMangas {
    public class MangareaderMangaLoadingStrategy : IMangaLoadingStrategy {
        public async Task LoadAsync(IManga manga, string path) {
            var doc = new HtmlDocument();
            var html = await GeneralFunctions.GetHtmlAsync(path);
            doc.LoadHtml(html);

            manga.Description = GeneralFunctions.FindNodes(doc, "div", "id", "readmangasum").ToList()[0].Descendants("p").ToList()[0].InnerText;
            manga.ImagePath = GeneralFunctions.FindNodes(doc, "div", "id", "mangaimg").ToList()[0].Descendants().ToList()[0].Attributes["src"].Value;
            manga.Chapters = GeneralFunctions.FindChapters(doc).Split(';');
        }
    }
}
