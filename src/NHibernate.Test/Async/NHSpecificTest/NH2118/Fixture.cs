﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH2118
{
	using System.Threading.Tasks;

	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		protected override void OnSetUp()
		{
			base.OnSetUp();

			using(var s = Sfi.OpenStatelessSession())
			using(var tx = s.BeginTransaction())
			{
				s.Insert(new Person {FirstName = "Bart", LastName = "Simpson"});
				s.Insert(new Person { FirstName = "Homer", LastName = "Simpson" });
				s.Insert(new Person { FirstName = "Apu", LastName = "Nahasapeemapetilon" });
				s.Insert(new Person { FirstName = "Montgomery ", LastName = "Burns" });
				tx.Commit();
			}
		}

		[Test]
		public async Task CanGroupByWithoutSelectAsync()
		{
			using(var s = Sfi.OpenSession())
			using (s.BeginTransaction())
			{
				var groups = await (s.Query<Person>().GroupBy(p => p.LastName).ToListAsync());
                
				Assert.AreEqual(3, groups.Count);
			}
		}

		protected override void OnTearDown()
		{
			base.OnTearDown();
			using(var s = Sfi.OpenStatelessSession())
			using (var tx = s.BeginTransaction())
			{
				s.CreateQuery("delete from Person").ExecuteUpdate();
				tx.Commit();
			}
		}
	}
}
