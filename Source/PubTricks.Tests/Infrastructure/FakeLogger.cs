using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubTricks.Web.Infrastructure;

namespace PubTricks.Tests.Infrastructure {
    public class FakeLogger : ILogger {
        public void LogDebug(string message) {
        }

        public void LogError(Exception x) {
        }

        public void LogError(string message) {
        }

        public void LogFatal(Exception x) {
        }

        public void LogFatal(string message) {
        }

        public void LogInfo(string message) {
        }

        public void LogWarning(string message) {
        }
    }
}
