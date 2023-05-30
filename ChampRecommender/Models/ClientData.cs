using System.Diagnostics;
using System.Security.Cryptography.Pkcs;
using static System.Net.WebRequestMethods;

namespace ChampRecommender.Models
{
    class ClientData
    {
        public const string CLIENT_NAME = "LeagueClientUx";

        public const string GAME_CLIENT_NAME = "League of Legends";
        public static string? LeaguePath { get; set; } // 롤 클라이언트 실행 경로
        public static string? ToKen { get; set; } // 롤 클라이언트 토큰
        public static ushort? Port { get; set; } // 롤 클라이언트 포트
        public static string? ApiUrl { get; set; } // 롤 클라이언트 ApiUrl

        // 메인 프로세스
        public static Process? clientProcess = null;

        // 게임 프로세스
        public static Process? gameClientProcess = null;
    }

    class ServerData
    {
        public static string ServerUrl = "https://mobilex.kr/ai/dev/team12/post_test";
    }
}
