using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChatApi {
    public class ChatApiService {
        public ChatApiService(string jsonPath) {
            SetOption(jsonPath);
        }
        public ChatApiService(ChatApiOption option, ChatBehaviorOption behaviorOption) {
            this.Option = option;
            this.BehaviorOption = behaviorOption;
        }
        public void SetOption(ChatApiOption option, ChatBehaviorOption behaviorOption ) {
            this.Option = option;
            this.BehaviorOption = behaviorOption;
        }

        public void SetOption(string jsonPath) {
            if (File.Exists(jsonPath)) {
                var jsonStr = File.ReadAllText(jsonPath);
                var jsonObject = JObject.Parse(jsonStr);
                if(jsonObject["chat_api_option"] != null) {
                    Option = JsonConvert.DeserializeObject<ChatApiOption>(jsonObject["chat_api_option"].ToString());
                }
                if (jsonObject["chat_behavior_option"] != null) {
                    BehaviorOption = JsonConvert.DeserializeObject<ChatBehaviorOption>(jsonObject["chat_behavior_option"].ToString());
                }
            }
          
        }

        internal ChatApiOption Option { get; private set; }
        internal ChatBehaviorOption BehaviorOption { get; private set; }

    }

    public class ChatApiOption {
        public ChatApiOption(string apiKey = "", string apiUrl = "") {
            ApiKey = apiKey;
            ApiUrl = apiUrl;
        }
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
        [JsonProperty("api_url")]
        public string ApiUrl { get; set; }

    }

    public class ChatBehaviorOption {
        [JsonProperty("first_promote")]
        public string FirstPromote { get; set; }
        [JsonProperty("defult_error_response")]
        public string DefultErrorResponse { get; set; } = @"暂时没有思绪呢~ ε=ε=ε=ε=ε=ε=┌(;￣◇￣)┘  (｡￫‿￩｡) （๑￫‿ฺ￩๑）（=ˇωˇ=）（⺻▽⺻ ）<(￣︶￣)>(•‾̑⌣‾̑•)✧˖° (๑˘ ˘๑)  ♥(｡￫v￩｡)♥";
        [JsonProperty("defult_connect_error_response")]
        public string DefultConnectErrorResponse { get; set; } = @"啊！我的脑子离开了我的身体，容我缓缓 (┬┬﹏┬┬) ಥ_ಥ (‾◡◝) /_ \ <(＿　＿)>";

    }

}
