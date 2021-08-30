using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AuthorChallenge
{
    class Result
    {
        /*
         * Complete the 'getUsernames' function below.
         *
         * The function is expected to return a STRING_ARRAY.
         * The function accepts INTEGER threshold as parameter.
         *
         * URL for cut and paste
         * https://jsonmock.hackerrank.com/api/article_users?page=<pageNumber>
         * 
         * 
         * Reference Files
         * 
         * https://github.com/tthompson899/hackerrank-articleAPI/blob/main/articleApi.py
         * 
         * https://github.com/sunday-okpoluaefe/authorChallenge.git
         */

        public static List<string> getUsernames(int threshold)
        {
            int page = 1;
            long total_page = 1;
            List<string> names = new List<string>();
            while (page <= total_page)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://jsonmock.hackerrank.com");
                    HttpResponseMessage response = client.GetAsync("/api/article_users?page=" + page).Result;
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;



                    JObject jsonObject = JObject.Parse(result);
                    //var obj = new ApiResponse(jsonObject);
                    if (page == 1)
                        total_page = (long)jsonObject.GetValue("total_pages");

                    JArray jsonArray = (JArray)jsonObject.GetValue("data");

                    foreach (var author in jsonArray)
                    {
                        if (Convert.ToInt16(author["submission_count"]) > threshold)
                            names.Add(author["username"].ToString());
                    }
                    //Console.WriteLine("Result: " + result);
                }
                page += 1;
            }
            return names;

        }

    }
    class Program
    {

        public static void Main(string[] args)
        {

            #region CodeSignal

            List<string> st = new List<string>();
            st.Add("dfadf|ERROR|FADSF.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|FADSF.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|WFQWD.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|WFQWD.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|DGAS.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|DSFGAS.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|SDGASD.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|GASDG.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|GASD.DF|FASDF|FASDF|FASDF");
            st.Add("dfadf|ERROR|ASDG.DF|FASDF|FASDF|FASDF");

            var abc = (from nod in st
                       where nod.Split('|')[1].ToLower() == "error"
                       select new { error = nod.Split('|')[1], file = nod.Split('|')[2] }).ToList();

            Console.WriteLine(abc.GroupBy(g => g.file));

            #endregion

            int threshold = Convert.ToInt32(Console.ReadLine().Trim());

            List<string> result = Result.getUsernames(threshold);

            Console.WriteLine(String.Join("\n", result));


            Console.ReadLine();
        }
    }

}
