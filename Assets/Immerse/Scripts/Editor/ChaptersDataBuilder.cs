using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Immerse.Brodsky.Data;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Immerse.Brodsky.Editor
{
    public static class ChaptersDataBuilder
    {
        [MenuItem("Immerse/BuildChaptersList")]
        public static void BuildChaptersList()
        {
            var chaptersList = new List<string>
            {
                "intro",
                "prologue",
                "chapter-1",
                "chapter-1-2",
                "chapter-2",
                "chapter-2-3",
                "chapter-3-1",
                "chapter-3-2",
                "chapter-3-4-1",
                "chapter-3-4-2",
                "chapter-4",
                "chapter-4-5",
                "nobel",
                "cat",
                "case",
                "piers"
               
            };

            string json = "";
            try
            {
                json = JsonConvert.SerializeObject(chaptersList);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}");
                return;
            }

            SaveToFile(DataLoadingStrings.ChaptersFilePath, json);
        }

        [MenuItem("Immerse/BuildIntro")]
        public static void BuildIntro()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 0,
                name = "Интро",
                hasAR = false,
                arDuration = "03:48",
                arAvailableAt = "03:45",
                
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 0,
                name = "Intro",
                hasAR = false,
                arDuration = "03:48",
                arAvailableAt = "03:45",
         
            };

            BuildChapter("intro", ruChapterData, enChapterData);
        }
        [MenuItem("Immerse/BuildPrologue")]
        public static void BuildPrologue()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 0,
                name = "Пролог",
                hasAR = false,
                arDuration = "03:48",
                arAvailableAt = "03:45",
                
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 0,
                name = "Prologue",
                hasAR = false,
                arDuration = "02:40",
                arAvailableAt = "02:40",
         
            };

            BuildChapter("prologue", ruChapterData, enChapterData);
        }
        
        [MenuItem("Immerse/BuildChapter-1")]
        public static void BuildChapter1()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 0,
                name = "Chapter-1",
                hasAR = false,
                arDuration = "04:26",
                arAvailableAt = "04:25",
                
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 0,
                name = "Chapter-1",
                hasAR = false,
                arDuration = "04:26",
                arAvailableAt = "04:25",
         
            };

            BuildChapter("chapter-1", ruChapterData, enChapterData);
        }
        
        [MenuItem("Immerse/BuildChapter1-2")]
        public static void BuildChapter1and2()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 1,
                name = "Chapter-1-2",
                hasAR = true,
                arDuration = "03:00",
                arAvailableAt = "02:26",
                comments = new List<CommentData>
                {
                    new CommentData()
                    {
                        enterTime = "01:33",
                        exitTime = "01:42",
                        header = "Marianna (Marina) Basmanova",
                        text = "Artist-illustrator. Born in 1938 in Leningrad (USSR)",
                        imagePath = "marina"
                    },
                   
                }
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 1,
                name = "Chapter-1-2",
                hasAR = true,
                arDuration = "03:00",
                arAvailableAt = "02:26",
                comments = new List<CommentData>
                {
                    new CommentData()
                    {
                        enterTime = "01:33",
                        exitTime = "01:42",
                        header = "Marianna (Marina) Basmanova",
                        text = "Artist-illustrator. Born in 1938 in Leningrad (USSR)",
                        imagePath = "marina"
                    },
                   
                }
            };

            BuildChapter("chapter-1-2", ruChapterData, enChapterData);
        }

        [MenuItem("Immerse/BuildChapter2")]
        public static void BuildChapter2()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 2,
                name = "Говорит Москва"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 2,
                name = "Chapter-2",
                hasAR = false,
                comments = new List<CommentData>
                {
                    new CommentData()
                    {
                        enterTime = "00:16",
                        exitTime = "00:28",
                        header = "Derek Walcott (Sir Derek Alton Walcott)",
                        text = "Poet, playwright. Born 1913 in Castries Saint Lucia, died 2017 in Cape Estate. Nobel Prize winner, T.S. Eliot Prize, awarded with Guggenheim Scholarship, etc",
                        imagePath = "derek"
                    },
                    new CommentData()
                    {
                        enterTime = "00:46",
                        exitTime = "00:54",
                        header = "Tomas Venclova",
                        text = "Lithuanian poet, prose defender and dissident. Born in 1937 in Klaipėda.",
                        imagePath = "tomas"
                    },
                    new CommentData()
                    {
                        enterTime = "01:41",
                        exitTime = "01:51",
                        header = "Natalia Gorbanevskaya",
                        text = "Poet, a translator of Polish literature and a civil-rights activist. Born in 1936 in Moscow (USSR), died in 2013 in Paris (France).",
                        imagePath = "gorb"
                    },
                    new CommentData()
                    {
                        enterTime = "02:56",
                        exitTime = "03:06",
                        header = "Lev Losev",
                        text = "Poet, literary critic, teacher. Born in 1937 in Leningrad, died in 2009 in New Hampshire (USA). Recipient of the Guggenheim Fellowship in the Humanities.",
                        imagePath = "lev"
                    },

                   
                }
                
                
            };

            BuildChapter("chapter-2", ruChapterData, enChapterData);
        }
        
        [MenuItem("Immerse/BuildChapter2-3")]
        public static void BuildChapter2and3()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 3,
                name = "Сцена 3"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 3,
                name = "Chapter2-3",
                hasAR = false,
            };

            BuildChapter("chapter-2-3", ruChapterData, enChapterData);
        }

        [MenuItem("Immerse/BuildChapter3-1")]
        public static void BuildChapter3and1()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 4,
                name = "Chapter3-1"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 4,
                name = "Chapter3-1",
                hasAR = false,
            };

            BuildChapter("chapter-3-1", ruChapterData, enChapterData);
        }
        [MenuItem("Immerse/BuildChapter3-2")]
        public static void BuildChapter3and2()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 4,
                name = "Chapter3-1"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 4,
                name = "Chapter3-2",
                hasAR = true,
                arDuration = "06:25",
                arAvailableAt = "00:40",
                comments = new List<CommentData>
                {
                    new CommentData()
                    {
                        enterTime = "02:36",
                        exitTime = "02:48",
                        header = "Mark Strand",
                        text = "Poet, essayist, translator. Born in 1934 in Summerside (Canada), died in 2014 in New York (USA). Pulitzer Prize winner, Bollingham Prize winner, awarded Guggenheim Scholarship, etc.",
                        imagePath = "mark"
                    },
                    new CommentData()
                    {
                        enterTime = "05:51",
                        exitTime = "05:58",
                        header = "Wystan Hugh Auden",
                        text = "Poet. Born in 1907 in York (United Kingdom), died in 1973 in Vienna (Austria). Pulitzer Prize winner, Bollingen Prize winner and Feltrinelli Prize winner, awarded Guggenheim Scholarship, Royal Gold Medal, etc.",
                        imagePath = "auden"
                    },
                  

                   
                }
            };

            BuildChapter("chapter-3-2", ruChapterData, enChapterData);
        }
        
        [MenuItem("Immerse/BuildChapter3-4-1")]
        public static void BuildChapter3and41()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 4,
                name = "Chapter3-4-1"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 4,
                name = "Chapter3-4-1",
                arDuration = "06:25",
                arAvailableAt = "00:40",
                comments = new List<CommentData>
                {
                    new CommentData()
                    {
                        enterTime = "00:32",
                        exitTime = "00:42",
                        header = "Czesław Miłosz",
                        text = "Polish poet, translator. Born in 1911 in Setenyi (Lithuania), died in 2004 in Krakow (Poland). Nobel Prize winner, Righteous Among the Nations.",
                        imagePath = "czeslav"
                    }
                }
            };

            BuildChapter("chapter-3-4-1", ruChapterData, enChapterData);
        }
        
        [MenuItem("Immerse/BuildChapter3-4-2")]
        public static void BuildChapter3and42()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 4,
                name = "Chapter3-4-2"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 4,
                name = "Chapter3-4-2",
                hasAR = true,
                arDuration = "02:00",
                arAvailableAt = "01:02",
               
            };

            BuildChapter("chapter-3-4-2", ruChapterData, enChapterData);
        }

        
        [MenuItem("Immerse/BuildChapter4")]
        public static void BuildChapter4()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 5,
                name = "Сцена 5"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 5,
                name = "Chapter 4"
            };

            BuildChapter("chapter-4", ruChapterData, enChapterData);
        }

        [MenuItem("Immerse/BuildChapter4-5")]
        public static void BuildChapter4and5()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 5,
                name = "Сцена 5"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 5,
                name = "Chapter 4-5",
                hasAR = false,
                comments = new List<CommentData>
                {
                    new CommentData()
                    {
                        enterTime = "00:09",
                        exitTime = "00:16",
                        header = "Susan Sontag",
                        text = "Writer, essayist, philosopher, literary critic, film and theater critic, screenwriter, theater and film director. Born in 1933 in New York City, she died in 2004 in New York City. Recipient of the MacArthur Award, the Jerusalem Prize, and the Prince of Asturias Award.",
                        imagePath = "susan"
                    },
                    new CommentData()
                    {
                        enterTime = "00:59",
                        exitTime = "01:04",
                        header = "Ludmila Shtern",
                        text = "Russian writer, translator, journalist. Born in 1935 in Leningrad (USSR).",
                        imagePath = "ludmila"
                    }
                }
            };

            BuildChapter("chapter-4-5", ruChapterData, enChapterData);
        }
        [MenuItem("Immerse/BuildChapterNobel")]
        public static void BuildChapterNobel()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 10,
                name = "Nobel"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 10,
                name = "Nobel",
                hasAR = true,
                arDuration = "02:00",
                arAvailableAt = "00:00",
                
            };

            BuildChapter("nobel", ruChapterData, enChapterData);
        }
        [MenuItem("Immerse/BuildChapterPiers")]
        public static void BuildChapterPiers()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 10,
                name = "piers"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 10,
                name = "piers",
                hasAR = true,
                arDuration = "02:00",
                arAvailableAt = "00:00",
                
            };

            BuildChapter("piers", ruChapterData, enChapterData);
        }
        [MenuItem("Immerse/BuildChapterCat")]
        public static void BuildChapterCat()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 10,
                name = "cat"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 10,
                name = "cat",
                hasAR = true,
                arDuration = "02:00",
                arAvailableAt = "00:00",
                
            };

            BuildChapter("cat", ruChapterData, enChapterData);
        }
        [MenuItem("Immerse/BuildChapterCase")]
        public static void BuildChapterCase()
        {
            var ruChapterData = new ChapterData()
            {
                sortingOrder = 10,
                name = "case"
            };

            var enChapterData = new ChapterData()
            {
                sortingOrder = 10,
                name = "case",
                hasAR = true,
                arDuration = "02:00",
                arAvailableAt = "00:00",
                
            };

            BuildChapter("case", ruChapterData, enChapterData);
        }
        private static void BuildChapter(string chapterName, ChapterData ruChapterData, ChapterData enChapterData)
        {
            var ruJson = JsonConvert.SerializeObject(ruChapterData);
            var enJson = JsonConvert.SerializeObject(enChapterData);

            string ruPath = "";
            string enPath = "";

            try
            {
                ruPath = DataLoadingStrings.GetChapterDataFilePath(BrodskyLocale.Russian, chapterName);
                enPath = DataLoadingStrings.GetChapterDataFilePath(BrodskyLocale.English, chapterName);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}");
                return;
            }

            SaveToFile(ruPath, ruJson);
            SaveToFile(enPath, enJson);
        }

        private static async Task SaveToFile(string path, string content)
        {
            try
            {
                await File.WriteAllTextAsync(path, content);
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}");
                return;
            }
            
            Debug.Log($"File saved: {path}");
        }
    }
}