using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatApi {
    public class ChatApiHelper {
        public class ApowersoftTaskData {
            public string task_id { get; set; }
        }

        public class ApowersoftTask {
            public int status { get; set; }
            public string message { get; set; }
            public ApowersoftTaskData data { get; set; }
        }
        public class ApowersoftResult {
            public int status { get; set; }
            public ApowersoftResultData data { get; set; }
        }

        public class ApowersoftResultData {
            public string text { get; set; }
            public string conversation_id { get; set; }
            public string msg_id { get; set; }
            public int state { get; set; }
            public string task_id { get; set; }
        }


        public ChatApiHelper(ChatApiService chatApiService) {
            this.chatApiService = chatApiService;
            if (string.IsNullOrEmpty(chatApiService.Option.ApiKey)){
                throw new Exception("Please set apiKey first!");
            }
            url = chatApiService.Option.ApiUrl;

            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", chatApiService.Option.ApiKey);
            //client.DefaultRequestHeaders.Add("UserAgent", "");
        }


        private string url = "";
        private HttpClient client;
        private ChatApiService chatApiService;

        /// <summary>
        /// 发送第一条人设 promote
        /// </summary>
        /// <returns></returns>
        public async Task<ApowersoftResultData> SetFirstPromote() {
            if (!string.IsNullOrEmpty(chatApiService?.BehaviorOption?.FirstPromote)) {
                return await GetResults(chatApiService?.BehaviorOption?.FirstPromote);
            }
            else {
                return null;
            }
        }

        public async Task<ApowersoftResultData> GetResults(string inputText, ApowersoftResultData lastData = null) {
            var result = new ApowersoftResultData();

            var taskId = await CreateTask(inputText, false, lastData);
            if (string.IsNullOrEmpty(taskId)) {
                result.text = chatApiService.BehaviorOption.DefultErrorResponse;
            }
            var data = await QueryTask(taskId);
            result = JsonConvert.DeserializeObject<ApowersoftResult>(data).data;
            var count = 1;
            while (result.state != 1 && count < 100) {
                data = await QueryTask(taskId);
                result = JsonConvert.DeserializeObject<ApowersoftResult>(data).data;
                await Task.Delay(100);
                count++;
            }


            return result;
        }

        public async Task<ApowersoftResultData> GetStreamResult(string inputText, Action<string> append, ApowersoftResultData lastData = null) {
            var result = new ApowersoftResultData();

            var taskId = await CreateTask(inputText, true, lastData);
            if (string.IsNullOrEmpty(taskId)) {
                result.text = chatApiService.BehaviorOption.DefultConnectErrorResponse;
            }
            var streamReader = await QueryTaskBySteam(taskId);

            while (true && streamReader != null) {
                var line = await streamReader.ReadLineAsync();
                if (line == null) break;

                // 解析 SSE 消息内容
                if (line.StartsWith("data: ")) {
                    var data = line.Substring("data: ".Length).Trim();
                    if (data.EndsWith("}")) {
                        result = JsonConvert.DeserializeObject<ApowersoftResultData>(data);
                    }
                    else {
                        append?.Invoke(data);
                    }
                }
                await Task.Delay(10);
            }
            return result;
        }

        private async Task<string> CreateTask(string inputText, bool isStream, ApowersoftResultData lastData = null) {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(inputText), "prompt");

            if (isStream) {
                form.Add(new StringContent("1"), "response_type");
            }
            if (lastData != null) {
                form.Add(new StringContent(lastData.conversation_id), "conversation_id");
                form.Add(new StringContent(lastData.msg_id), "prev_msg_id");
            }
            try {
                var response = await client.PostAsync(url, form);
                var result = JsonConvert.DeserializeObject<ApowersoftTask>(await response.Content.ReadAsStringAsync());
                return result.data.task_id;
            }
            catch (System.Exception ex) {
                Debug.WriteLine(ex);
                return string.Empty;
            }
        }

        private async Task<StreamReader> QueryTaskBySteam(string id) {
            var request = new HttpRequestMessage(HttpMethod.Get, url + id) {
                Headers = {
                    {"Accept", "text/event-stream" }
                }
            };
            try {
                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var stream = await response.Content.ReadAsStreamAsync();
                var reader = new StreamReader(stream);
                return reader;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return null;
            }
        }

        private async Task<string> QueryTask(string id) {

            try {
                return await client.GetStringAsync(url + id);
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
