using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChatWindow.Helper {
    public class ApiHelper {
        public class ApowersoftResult {
            public int status { get; set; }
            public ApowersoftResultData data { get; set; }
        }
        public class ApowersoftResultData {
            public string text { get; set; }
            public int state { get; set; }
        }
        public async Task<string> GetResults(string inputText) {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", "你的key");
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(inputText), "prompt");
            form.Add(new StringContent("1"), "sync");
            try {
                var response = await client.PostAsync("https://aw.aoscdn.com/tech/tasks/gpt/completion", form);
                var result = await response.Content.ReadFromJsonAsync<ApowersoftResult>();
                return result.data.text;
            } catch (System.Exception) {

                return "暂时没有思绪呢~ ε=ε=ε=ε=ε=ε=┌(;￣◇￣)┘  (｡￫‿￩｡) （๑￫‿ฺ￩๑）（=ˇωˇ=）（⺻▽⺻ ）<(￣︶￣)>(•‾̑⌣‾̑•)✧˖° (๑˘ ˘๑)  ♥(｡￫v￩｡)♥";
            }
           
        }
    }
}
