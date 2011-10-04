using System;
using System.Diagnostics;
using NUnit.Framework;

namespace PubTricks.Tests {
    public class TestBase {
        public void Describes(string description) {
            Console.WriteLine("-------------------------------");
            Console.WriteLine(description);
            Console.WriteLine("-------------------------------");
        }

        public void IsPending() {
            Console.WriteLine(GetCaller() + " -- PENDING --");
            Assert.Inconclusive();
        }

        public string GetCaller() {
            StackTrace stack = new StackTrace();
            return stack.GetFrame(2).GetMethod().Name.Replace("_", " ");
        }
    }
}
