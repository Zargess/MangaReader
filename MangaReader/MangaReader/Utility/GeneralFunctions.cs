using HtmlAgilityPack;
using MangaReader.Interfaces;
using MangaReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaReader.Utility {
    public class GeneralFunctions {
        public static async Task<List<IManga>> GenerateMangaListAndSaveResult() {
            var list = await GenerateMangaList();
            // TODO : Save result
            return list;
        }

        private static async Task<List<IManga>> GenerateMangaList() {
            var res = new List<IManga>();
            var doc = new HtmlDocument();
            var html = await GetHtml(Constants.MANGALIST);
            doc.LoadHtml(html);

            var mangalists = FindNodes(doc, "ul", "class", "series_alpha");
            var mangalinks = new List<string>();
            foreach (var node in mangalists) {
                var linktags = node.Descendants("a").Where(x => x.Attributes.Contains("href"));
                var mangas = linktags.Select(x => CreateMangaFromTag(x));
                res.AddRange(mangas);
            }

            return res;
        }

        private static IManga CreateMangaFromTag(HtmlNode x) {
            var link = Constants.WEBSITE + x.Attributes["href"].Value;
            var title = x.InnerText;
            return new Manga(title, link);
        }

        public static string FindChapters(HtmlDocument doc) {
            var res = "";

            var chapterlist = FindNodes(doc, "div", "id", "chapterlist").ToList()[0];
            var chapterlinktags = chapterlist.Descendants("a").Where(x => x.Attributes.Contains("href"));

            foreach (var tag in chapterlinktags) {
                res += Constants.WEBSITE + tag.Attributes["href"].Value + ";";
            }

            return res;
        }

        public static IEnumerable<HtmlNode> FindNodes(HtmlDocument doc, string tag, string attribute, string attrvalue) {
            return doc.DocumentNode.Descendants(tag).Where(x => x.Attributes.Contains(attribute) && x.Attributes[attribute].Value.Contains(attrvalue));
        }

        public static async Task<string> GetHtml(string link) {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(link);
            request.Method = "GET";
            var response = await request.GetResponseAsync();
            
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
    }
}