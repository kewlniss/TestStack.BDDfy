using Bddify;
using NUnit.Framework;

namespace SutBehaviors
{
    public static class BddifyNunit
    {
        public static Bddifier Bddify<T>(this object testObject)
            where T : IScanner, new()
        {
            var bddifier = new Bddifier(
                testObject,
                new T(),
                new IProcessor[]
                { 
                    new TestRunner<InconclusiveException>(), 
                    new ConsoleReporter(),
                    new HtmlReporter("d:\\temp"),
                    new ExceptionHandler(Assert.Inconclusive)
                });

            bddifier.Run();
            return bddifier;
        }
    }
}