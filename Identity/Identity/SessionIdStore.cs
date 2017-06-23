using Identity.Model;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Timers;

namespace Identity.Identity
{
    public static class SessionIdStore
    {
        private static ConcurrentDictionary<Token, string> SessionIdList = new ConcurrentDictionary<Token, string>();

        static SessionIdStore()
        {
            var timer = new Timer
            {
                Interval = new TimeSpan(0, 0, 60).TotalMilliseconds,
                AutoReset = true
            };

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }
        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var value = string.Empty;
            var invalidsTokens = SessionIdList.Where(p => p.Key.Validity >= DateTime.Now).Select(p => p.Key).ToList();
            invalidsTokens.ForEach(p => SessionIdList.TryRemove(p, out value));
        }

        public static void AddSessionId(Token token, string sessionId) => SessionIdList.TryAdd(token, sessionId);
        public static string GetSessionId(Token token)
        {
            var sessionId = string.Empty;
            if (SessionIdList.TryGetValue(token, out sessionId)) return sessionId;
            return null;
        }
    }
}
