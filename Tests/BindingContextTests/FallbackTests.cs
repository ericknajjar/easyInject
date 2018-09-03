using System;
using NUnit.Framework;
using EasyInject.IOC.extensions;
using EasyInject.IOC;

namespace EasyInject.Tests.BindingContextTests
{
    [TestFixture()]
    public class FallbackTests
    {
            [Test()]
            public void BindingContextFallsBackToFallback()
            {
                var context = TestsFactory.BindingContext();

                context.FallBack((name, key, extras) => {
                    return 3;
                });

                var result = context.Get<int>();

                Assert.AreEqual(result, 3);
            }

            [Test()]
            public void CanFallbakcToMultipleTypes()
            {
                var context = TestsFactory.BindingContext();

                context.FallBack((name, key, extras) => {

                    if (key.Key.Equals(typeof(string)))
                        return "str";

                    return null;
                });

                context.FallBack((name, key, extras) => {
                    
                    if(key.Key.Equals(typeof(int)))
                        return 3;

                    return null;
                });

              
                var result = context.Get<string>();

                Assert.AreEqual(result, "str");
            }

            [Test()]
            public void FallbackToAnotherContext()
            {
                var context1 = TestsFactory.BindingContext();
                var context2 = TestsFactory.BindingContext();

                context1.Bind<string>().To("str");

                context2.FallBack(context1);

                var result = context2.Get<string>();

                Assert.AreEqual(result, "str");
            }

            [Test()]
            public void FallbackToAnotherWorksEvenIfFirstContextDoesntContain()
            {
                var context1 = TestsFactory.BindingContext();
                var context2 = TestsFactory.BindingContext();
                var context3 = TestsFactory.BindingContext();

                context2.Bind<string>().To("str");

                context3.FallBack(context1);
                context3.FallBack(context2);

                var result = context3.Get<string>();

                Assert.AreEqual(result, "str");
            }

            [Test()]
            public void FallbackConsiderNames()
            {
                var context1 = TestsFactory.BindingContext();
                var context2 = TestsFactory.BindingContext();

                context1.Bind<string>().To("str");
                context1.Bind<string>("name").To("str2");

                context2.FallBack(context1);

                var result = context2.Get<string>("name");

                Assert.AreEqual(result, "str2");
            }

            [Test()]
            public void FallbackUseExtras()
            {
                var context1 = TestsFactory.BindingContext();
                var context2 = TestsFactory.BindingContext();

                System.Func<string,string> ret = (string extras) => extras;

                var binding = new Binding(ret);
                var key = new BindingKey(typeof(string));

                context1.Unsafe.Bind(key).To(binding);
                context2.FallBack(context1);

                var result = context2.Get<string>(InnerBindingNames.Empty,"abc");

                Assert.AreEqual(result, "abc");
            }


        }
    }


