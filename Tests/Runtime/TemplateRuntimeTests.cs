using NUnit.Framework;

namespace HTDA.Framework.SceneFlow.Tests
{
    public class TemplateRuntimeTests
    {
        [Test]
        public void PackageInfo_Name_IsNotEmpty()
        {
            Assert.IsFalse(string.IsNullOrEmpty(HTDA.Framework.SceneFlow.PackageInfo.Name));
        }
    }
}