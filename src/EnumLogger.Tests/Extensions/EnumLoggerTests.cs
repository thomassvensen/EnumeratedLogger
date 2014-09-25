using System;
using EnumLogger.Attributes;
using EnumLogger.Extensions;
using EnumLogger.Tests.Configuration;
using NUnit.Framework;

namespace EnumLogger.Tests.Extensions
{
    [TestFixture]
    [LogToConsole]
    public class EnumLoggerTests
    {
        [Test]
        public void CheckOutputFromVariousLogCalls()
        {
            DummyLogEventEnum.FirstEvent.Log(this, "logging in {0}", "Steve", new Exception("ERROR!"));
            DummyLogEventEnum.SecondEvent.Log(this, "Submitting some data...");
            DummyLogEventEnum.ThirdEvent.Log<EnumLoggerTests>();
            DummyLogEventEnum.ThirdEvent.Log<EnumLoggerTests>("Again");
        }
    }

}