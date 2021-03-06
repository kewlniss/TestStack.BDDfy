﻿using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestStack.BDDfy.Samples
{
    public class AsyncExample
    {
        private Sut _sut;

        public async void GivenSomeAsyncSetup()
        {
            _sut = await CreateSut();
        }

        public void ThenBddfyHasWaitedForThatSetupToCompleteBeforeContinuing()
        {
            Assert.NotNull(_sut);
        }

        public async Task AndThenBddfyShouldCaptureExceptionsThrownInAsyncMethod()
        {
            await Task.Yield();
            throw new Exception("Exception in async void method!!");
        }

        private async Task<Sut> CreateSut()
        {
            await Task.Delay(500);
            return new Sut();
        }

        [Test]
        public void Run()
        {
            try
            {
                // we need TestObject for this test; so I disable StoryCache processor for this one test
                Configuration.Configurator.Processors.StoryCache.Disable();
                var engine = this.LazyBDDfy();
                var exception = Assert.Throws<Exception>(() => engine.Run());

                Assert.AreEqual("Exception in async void method!!", exception.Message);
            }
            finally
            {
                Configuration.Configurator.Processors.StoryCache.Enable();
            }
        }

        internal class Sut
        {
        }
    }
}