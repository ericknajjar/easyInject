﻿using NUnit.Framework;
using System;
using EasyInject.IOC;
using EasyInject.IOC.extensions;

namespace EasyInject.Tests.BindingContextTests
{
	[TestFixture ()]
	public class Tests
	{

		[Test ()]
		public void BindingSimpleInt ()
		{
			IBindingContext context = TestsFactory.BindingContext();

			int expected = 45;

			context.Bind<int>().To(() => expected);

			Assert.AreEqual(context.Get<int>(), expected);
		}

		[Test ()]
		public void BindingSimpleIntError ()
		{
			IBindingContext context = TestsFactory.BindingContext();
			Assert.Throws<BindingNotFound>(() => context.Get<int>());
		}

		[Test ()]
		public void NamedBindingInt ()
		{
			IBindingContext context = TestsFactory.BindingContext();

			int expected = 45;
			string bindingName = "foo";

			context.Bind<int>(bindingName).To(() => expected);

			Assert.AreEqual(context.Get<int>(bindingName), expected);
		}

		[Test ()]
		public void NamedBindingIntError()
		{
			IBindingContext context = TestsFactory.BindingContext();

			Assert.Throws<BindingNotFound>(() => context.Get<int>("foo"));
		}

		[Test ()]
		public void NamedBindingDifferentTypeError()
		{
			IBindingContext context = TestsFactory.BindingContext();

			context.Bind<string>("foo").To(()=> "string");

			Assert.Throws<BindingNotFound>(() => context.Get<int>("foo"));
		}

		[Test ()]
		public void NamedBindingDifferentNameError()
		{
			IBindingContext context = TestsFactory.BindingContext();

			context.Bind<int>("foo").To(()=> 45);

			Assert.Throws<BindingNotFound>(() => context.Get<int>("notFoo"));
		}

		[Test ()]
		public void OneArgumentBinding()
		{
			IBindingContext context = TestsFactory.BindingContext();

            context.Bind<int>().With<string>().To((strParam, other)=> 45);

			context.Bind<string>().To(()=> "uhul");

			int ret = context.Get<int>();

			Assert.AreEqual(ret, 45);
		}

		[Test ()]
		public void OneArgumentBindingError()
		{
			//Requires a string to get the int binging

			IBindingContext context = TestsFactory.BindingContext();

			context.Bind<int>().With<string>().To((value)=> 45);

			Assert.Throws<BindingNotFound>(() =>context.Get<int>());
		}

		[Test ()]
		public void OneArgumentNamedBinding()
		{
			IBindingContext context = TestsFactory.BindingContext();

			int expected = 45;

			context.Bind<int>().With<string>("MyText").To((value) => expected);

			context.Bind<string>("MyText").To(()=> "uhul");

			int ret = context.Get<int>();

			Assert.AreEqual(ret, expected);
		}
		
		[Test ()]
		public void OneCorrectArgumentBinding()
		{
			IBindingContext context = TestsFactory.BindingContext();
			
			string parameter = "";
			
			context.Bind<int>().With<string>().To((value)=>{ parameter = value; return 45;});
			
			context.Bind<string>().To(()=> "uhul");
			
			context.Get<int>();
			
			Assert.AreEqual(parameter, "uhul");
		}

		[Test ()]
		public void OneCorrectArgumentNamedBinding()
		{
			IBindingContext context = TestsFactory.BindingContext();

			string parameter = "";

			context.Bind<int>().With<string>("MyText").To((value)=>{ parameter = value; return 45;});

			context.Bind<string>().To(()=> "uhul");

			context.Bind<string>("MyText").To(()=> "uhul2");

			context.Get<int>();

			Assert.AreEqual(parameter, "uhul2");
		}

		[Test ()]
		public void RequireItselfError()
		{
			IBindingContext context = TestsFactory.BindingContext();

			Assert.Throws<BindingSelfRequirement>( () => context.Bind<int>().With<int>().To((value)=> 45) );
		}

		[Test ()]
		public void RequireItselfWithSameNameError()
		{
			IBindingContext context = TestsFactory.BindingContext();

			Assert.Throws<BindingSelfRequirement>( () => context.Bind<int>("name").With<int>("name").To((value)=> 45));
		}

		[Test ()]
		public void RequireItselfWithDifferentNamesNoError()
		{
			IBindingContext context = TestsFactory.BindingContext();

			Assert.DoesNotThrow( () => context.Bind<int>("name").With<int>("differentName").To((value)=> 45));
		}

		[Test ()]
		public void TwoArgumentBinding()
		{
			IBindingContext context = TestsFactory.BindingContext();

			context.Bind<int>().With<string>().With<float>().To((str,flt)=> 45);

			context.Bind<string>().To(()=> "uhul");

			context.Bind<float>().To(()=> 3.0f);

			int ret = context.Get<int>();

			Assert.AreEqual(ret, 45);
		}

		[Test ()]
		public void TwoRequireItseflWithSameNameError()
		{
			IBindingContext context = TestsFactory.BindingContext();

			Assert.Throws<BindingSelfRequirement>( () => context.Bind<int>().With<string>().With<int>().To((str,i)=> 45));
		}

		[Test ()]
		public void TwoRequireItselfWithDifferentNamesNoError()
		{
			IBindingContext context = TestsFactory.BindingContext();

			Assert.DoesNotThrow( () => context.Bind<int>("name").With<string>().With<int>("diferentName").To((str,i)=> 45));
		}

		[Test ()]
		public void TwoRequireItselfWithDifferentNameNoError2()
		{
			IBindingContext context = TestsFactory.BindingContext();

			Assert.DoesNotThrow( () => context.Bind<int>("name").With<int>().With<int>().To((i,j)=> 45));
		}

		[Test ()]
		public void UnsafeSimpleBinding()
		{
			IBindingContext context = TestsFactory.BindingContext();

			System.Func<int> func = () => 45;
			IBinding binding = new Binding(func);

			context.Unsafe.Bind(typeof(int)).To(binding);

			Assert.AreEqual(context.Get<int>(), 45);
		}

		[Test ()]
		public void UnsafeNamedBinding()
		{
			IBindingContext context = TestsFactory.BindingContext();

			System.Func<int> func = () => 45;
			IBinding binding = new Binding(func);

			context.Unsafe.Bind("name",typeof(int)).To(binding);

			Assert.AreEqual(context.Get<int>("name"),45);
		}

		[Test ()]
		public void UnsafeSelfRequirementBindingError()
		{
			IBindingContext context = TestsFactory.BindingContext();
			IBindingRequirement requirement = BindingRequirements.Instance.With<int>();

			System.Func<int> func = () => 45;

			IBinding binding = new Binding(func,requirement);

			Assert.Throws<BindingSelfRequirement>(() => context.Unsafe.Bind(typeof(int)).To(binding));
		}

		[Test ()]
		public void UnsafePartialBinding()
		{
			IBindingContext context = TestsFactory.BindingContext();
			IBindingRequirement requirement = BindingRequirements.Instance.With<float>();

			context.Bind<float>().To(()=> 0.1f);
			int extra = -1;
			System.Func<float,int,int> func = (bindinded,nonBinded) => {extra = nonBinded;return 45;};

			IBinding binding = new Binding(func,requirement);

			context.Unsafe.Bind(typeof(int)).To(binding);

			context.Get<int>(InnerBindingNames.Empty,32);

			Assert.AreEqual(32,extra);
		}

		[Test ()]
		public void GetItself()
		{
			var context =  TestsFactory.BindingContext();

			IBindingContext meAgain = context.Get<IBindingContext>(InnerBindingNames.CurrentBindingContext);

			Assert.AreEqual(context,meAgain);
		}

		[Test ()]
		public void GetItselfEmptyName()
		{
			var context =  TestsFactory.BindingContext();

			IBindingContext meAgain = context.Get<IBindingContext>();

			Assert.AreEqual(context,meAgain);
		}


		public T TypedGet<T> (IBindingContext context)
		{
			return context.Get<T>();
		}

		public T TypedInterfaceGet<T> (IBindingContext context) where T:ITestInterface
		{
			return context.Get<T>();
		}

		[Test ()]
		public void TypedGetTest()
		{
			var context =  TestsFactory.BindingContext();
			
			context.Bind<int> ().To (() => 42);

			Assert.DoesNotThrow(() => TypedGet<int>(context) );
		}

        [Test()]
        public void UnsafeTypedGetTest()
        {
            var context = TestsFactory.BindingContext();

            context.Bind<int>().To(() => 42);

            var name = new BindingName(InnerBindingNames.Empty);
            var key = new BindingKey(typeof(int));
                
            Assert.DoesNotThrow(() => context.Unsafe.TryGet(name,key,new object[0]));
        }

		[Test ()]
		public void TypedInterfaceGetTest()
		{
			var context =  TestsFactory.BindingContext();
			
			var bind = context.Bind<ITestInterface> ();

			bind.To (() => new TestClass());
			
			Assert.DoesNotThrow(() => TypedGet<ITestInterface>(context) );
		}
         
		[Test ()]
		public void TryGetNotFoundTest()
		{
			var context =  TestsFactory.BindingContext();
			int exit = 0;
			Assert.DoesNotThrow(() => context.TryGet<int>(out exit) );
		}

		[Test ()]
		public void TryGetFoundTest()
		{
			var context =  TestsFactory.BindingContext();
			context.Bind<int>().To(() => 93);
			int exit = 0;
			Assert.That( context.TryGet<int>(out exit) );
		}

		[Test ()]
		public void TryGetFoundRightValueTest()
		{
			var context =  TestsFactory.BindingContext();
			context.Bind<int>().To(() => 93);
			int exit = 0;
			context.TryGet<int>(out exit);
			Assert.AreEqual(93,exit);
		}

		[Test ()]
		public void TryGetNotFoundReturnFalseTest()
		{
			var context =  TestsFactory.BindingContext();
			int exit = 0;

			Assert.That(!context.TryGet<int>(out exit));
		}

		[Test ()]
		public void TryGetNamedNotFoundReturnFalseTest()
		{
			var context =  TestsFactory.BindingContext();
			int exit = 0;

			Assert.That(!context.TryGet<int>("testName",out exit));
		}

        [Test()]
        public void BindToAnObjectReturnsTheRightObject()
        {
            var context = TestsFactory.BindingContext();
            context.Bind<int>().To(33);

            Assert.AreEqual(33, context.Get<int>());
        }

        [Test()]
        public void BindToSingletonReturnsTheSame()
        {
            var context = TestsFactory.BindingContext();
            var val = 0;

            context.Bind<int>().ToSingleton(()=> val++);
            var first = context.Get<int>();
            var second = context.Get<int>();

            Assert.AreEqual(first, second);
        }
         
        [Test()]
        public void BindToSingletonReturnsTheSameOneWith()
        {
            var context = TestsFactory.BindingContext();
            var val = 0;

            context.Bind<float>().To(1.0f);

            context.Bind<int>().With<float>().ToSingleton((f) => val++);

            var first = context.Get<int>();
            var second = context.Get<int>();

            Assert.AreEqual(first, second);
        }

        [Test()]
        public void BindToSingletonReturnsTheSameTwoWith()
        {
            var context = TestsFactory.BindingContext();
            var val = 0;

            context.Bind<float>().To(1.0f);
            context.Bind<string>().To("");

            context.Bind<int>().With<float>().With<string>().ToSingleton((f,s) => val++);

            var first = context.Get<int>();
            var second = context.Get<int>();

            Assert.AreEqual(first, second);
        }

        [Test()]
        public void CanGetValueFromSubcontext()
        {
            var context = TestsFactory.BindingContext();
            var subcontext = context.GetSubcontext("a");
      
            subcontext.Bind<int>().To(25);

            var first = context.GetSubcontext("a").Get<int>();
            Assert.AreEqual(25, first);
        }

        [Test()]
        public void SubcontextHasMainContextAsFallback()
        {
            var context = TestsFactory.BindingContext();
            context.Bind<string>().To("str");

            var subcontext = context.GetSubcontext("a");

            var str = subcontext.Get<string>();

            Assert.AreEqual(str, "str");
        }

        [Test()]
        public void SubcontextFallbacksAllTheWayUp()
        {
            var context = TestsFactory.BindingContext();
            context.Bind<string>().To("str");

            var subcontext = context.GetSubcontext("a","b");

            var str = subcontext.Get<string>();

            Assert.AreEqual(str, "str");
        }



        [Test()]
        public void TryGetPartialBindingTest()
        {
            IBindingContext context = TestsFactory.BindingContext();
            IBindingRequirement requirement = BindingRequirements.Instance.With<float>();

            context.Bind<float>().To(() => 0.1f);

            int extra = -1;

            System.Func<float, int, int> func = (bindinded, nonBinded) => { extra = nonBinded; return 45; };

            IBinding binding = new Binding(func, requirement);

            context.Unsafe.Bind(typeof(int)).To(binding);

            int exit = 0;

            context.TryGet<int>(out exit, 32);

            Assert.AreEqual(32, extra);
        }

	}

}

