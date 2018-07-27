using NUnit.Framework;
using System;
using EasyInject.IOC;
using EasyInject.IOC.extensions;

namespace EasyInject.Tests.BindingTests
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public void BindingRequirementEquality()
		{

			IBindingRequirement requirement = BindingRequirements.Instance.With<int>();
			IBindingRequirement requirementb = BindingRequirements.Instance.With(InnerBindingNames.Empty,typeof(int));

			Assert.AreEqual(requirement,requirementb);
		}

		[Test]
		public void BindingCheckForErrors()
		{

			IBindingRequirement requirement = BindingRequirements.Instance.With<int>();
	
			System.Func<int> func = () => 45;

			IBinding binding = new Binding(func,requirement);

			Assert.Throws<BindingSelfRequirement>(() => binding.CheckRequiremets(typeof(int),InnerBindingNames.Empty));
		}

        [Test]
        public void GetWorks()
        {
            Func<int> func = () => 45;

            IBinding binding = new Binding(func);

            var mock = new Moq.Mock<IBindingContext>();

             var value = (int)binding.Get(mock.Object);

            Assert.AreEqual(45, value);
        }

        [Test]
        public void BindingAsSingletonRetunsAlwaysTheSame()
        {
            var ret = 0;
            Func<int> func = () => ret++;

            var singletonBinding = new SingletonBinding(new Binding(func));

            var mock = new Moq.Mock<IBindingContext>();

            var value = (int)singletonBinding.Get(mock.Object);
            var value2 = (int)singletonBinding.Get(mock.Object);

            Assert.AreEqual(value, value2);
        }


	}
}

