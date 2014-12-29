using HtmlAgilityPack;
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
        public static async void GenerateMangaListAndSaveResult() {
            var list = await Task.Run(() => GenerateMangaList());
            foreach (var manga in list) {
                App.ViewModel.AllMangas.Add(manga);
            }
        }

        private static async Task<List<Manga>> GenerateMangaList() {
            var res = new List<Manga>();
            var doc = new HtmlDocument();
            doc.LoadHtml(GetHtml(Constants.MANGALIST));

            var mangalists = FindNodes(doc, "ul", "class", "series_alpha");
            var mangalinks = new List<string>();
            foreach (var node in mangalists) {
                var links = node.Descendants("a").Where(x => x.Attributes.Contains("href")).Select(x => Constants.WEBSITE + x.Attributes["href"].Value).ToList();
                mangalinks.AddRange(links);
            }

            foreach (var node in mangalists) {
                var links = node.Descendants("a").Where(x => x.Attributes.Contains("href")).Select(x => Constants.WEBSITE + x.Attributes["href"].Value).ToList();
                Debug.WriteLine("Links count: " + links.Count);
                foreach (var link in links) {
                    res.Add(CreateMangaFromDocument(link));
                }
                // TODO : Remove this later
                break;
            }


            return res;
        }

        public static Manga CreateMangaFromDocument(string link) {
            var doc = new HtmlDocument();
            doc.LoadHtml(GetHtml(link));

            var namenodes = FindNodes(doc, "h2", "class", "aname").ToList();
            var name = namenodes[0].InnerText;

            var descnodescontainer = FindNodes(doc, "div", "id", "readmangasum").ToList();
            var description = descnodescontainer[0].Descendants("p").ToList()[0].InnerText;

            var imgcontainer = FindNodes(doc, "div", "id", "mangaimg").ToList()[0];
            var image = imgcontainer.Descendants().ToList()[0].Attributes["src"].Value;

            var chapters = FindChapters(doc);

            var chapternumber = 1;
            Debug.WriteLine("Generated {0}", name);
            return new Manga(name, description, image, chapters, chapternumber);
        }

        private static List<string> FindChapters(HtmlDocument doc) {
            var res = new List<string>();

            var chapterlist = FindNodes(doc, "div", "id", "chapterlist").ToList()[0];
            var chapterlinktags = chapterlist.Descendants("a").Where(x => x.Attributes.Contains("href"));

            foreach (var tag in chapterlinktags) {
                res.Add(Constants.WEBSITE + tag.Attributes["href"].Value);
            }

            return res;
        }

        private static IEnumerable<HtmlNode> FindNodes(HtmlDocument doc, string tag, string attribute, string attrvalue) {
            return doc.DocumentNode.Descendants(tag).Where(x => x.Attributes.Contains(attribute) && x.Attributes[attribute].Value.Contains(attrvalue));
        }

        private static string GetHtml(string link) {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(link);
            request.Method = "GET";
            var task = request.GetResponseAsync();
            task.Wait();
            var res = task.Result;
            using (Stream stream = res.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
    }
}