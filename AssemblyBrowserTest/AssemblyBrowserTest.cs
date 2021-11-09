using NUnit.Framework;
using AssemblyAnalyzer.Containers;
using System.Collections.Generic;
using AssemblyAnalyzer;
using System;

namespace AssemblyBrowserTest
{
    public class AssemblyBrowserTest
    {
        private const string _testAssemblyPath = "/resource/UIntGeneratorPlugin.dll";
        private AssemblyBrowser _assemblyBrowser;
        private List<Container> _assemblyInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            _assemblyBrowser = new AssemblyBrowser();
            _assemblyInfo = _assemblyBrowser.GetAssemblyInfo(AppDomain.CurrentDomain.BaseDirectory + _testAssemblyPath);
        }

        [Test]
        public void AssemblyIsNotEmptyTest()
        {
            int expected = 0;
            int actual = _assemblyInfo.Count;
            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void AssemblyHasCorrectClassesNumberTest()
        {
            int expected = 1;
            int actual = _assemblyInfo[0].Members.Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AssemblyClassHasCorrectMembersNumberTest()
        {
            int expected = 4;
            Container container = (Container) _assemblyInfo[0].Members[0];
            int actual = container.Members.Count;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AssemblyClassHasSpecificMember()
        {
            string expectedMemberSignature = "public virtual Object GetRandomValue ()";
            Container container = (Container)_assemblyInfo[0].Members[0];
            bool hasMember = false;
            foreach (MemberInfo member in container.Members)
            {
                if (member.Signature.Equals(expectedMemberSignature))
                {
                    hasMember = true;
                    break;
                }
            }
            Assert.IsTrue(hasMember);
        }
    }
}