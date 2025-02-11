using ProcessingDataForWEB_APP;
using System;
using System.Collections.Concurrent;

namespace DataForWeb
{
    public class FM
    {
        public static void Main()
        {
            F f = new F();
            var a = f.SetF();


            Dictionary dictionary = new Dictionary();
            var test1 = dictionary.CreateMovieDictionary(a);
            
            var aaaaaa1 = test1.ElementAt(1).Value;
            var aaaaaa2 = test1.ElementAt(8).Value;

            var testSecondDict = dictionary.CreateActorMovieDictionary(a);

            var b1 = testSecondDict.ElementAt(1).Value;
            var b2 = testSecondDict.ElementAt(12).Value;

            var bdd= 0;




            //ActorsDirectorsNames test1 = new ActorsDirectorsNames();
            //string filePath = "C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\ActorsDirectorsNames_IMDB.txt";
            //var a = test1.SetIdName(filePath);

            //ActorsDirectorsCodes test2 = new ActorsDirectorsCodes();
            //string path = "C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\ActorsDirectorsCodes_IMDB.tsv";
            //var b = test2.SetIdRole(path);

            //linksIMDBMovieLens test3 = new linksIMDBMovieLens();
            //string path = "C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\links_IMDB_MovieLens.csv";
            //var c = test3.SetLinks(path);

            //MovieCodesIMDB test4 = new MovieCodesIMDB();
            //string path = "C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\MovieCodes_IMDB.tsv";
            //var d = test4.SetMovieCodes(path);

            //RatingsIMDB test5 = new RatingsIMDB();
            //string path = "C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\Ratings_IMDB.tsv";
            //var e = test5.SetRatings(path);

            //TagCodesMovieLens test6 = new TagCodesMovieLens();
            //string path = "C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\TagCodes_MovieLens.csv";
            //var f = test6.SetTagCodes(path);

            //TagScoresMovieLens test7 = new TagScoresMovieLens();
            //string path = "C:\\Users\\Borru\\OneDrive\\Desktop\\ml-latest\\TagScores_MovieLens.csv";
            //var g = test7.SetTagScores(path);
        }
    }
}