using NLayerApp.DAL.Repositories;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Entities;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.Services;
using NLayerApp.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BLL_UnitTest {
	[TestClass]
	public class AfishaServiceTest {
		// fields, that are needed by all tests
		private IUnitOfWork uof;
		private IAfishaService afisha;

		private int TestedTicketId = 236;

		[TestInitialize]
		public void TestInitialize() {
			uof = new EFUnitOfWork();
			afisha = new AfishaService();
			TestedTicketId = uof.Tickets.GetAll().Last().Id - 1;
		}

		[TestMethod]
		public void TestBookedTicketNormal() {
			// retrieve current count of booked tickets
			Ticket testedTicket = uof.Tickets.Get(TestedTicketId);
			int testedTicketBookedBefore = testedTicket.BookedCount;

			// book a ticket by AfishaSvc
			afisha.BookTicket(TestedTicketId);

			Assert.AreEqual(testedTicketBookedBefore + 1, testedTicket.BookedCount,
				$"(Old ticket count + 1) != (New ticket count)!");
		}

		[TestMethod]
		public void TestBookedTicketOverflow() {
			// retrieve current count of booked tickets
			Ticket testedTicket = uof.Tickets.Get(TestedTicketId + 1);
			int testedTicketBookedBefore = testedTicket.BookedCount;

			// book the (Last + 1) ticket by AfishaSvc
			afisha.BookTicket(TestedTicketId + 1);

			Assert.AreEqual(testedTicketBookedBefore, testedTicket.BookedCount,
				$"(Old ticket count) != (New ticket count) when there are no ticket left!");
		}

		[TestMethod]
		public void TestBuyTicket() {
			// retrieve current count of booked tickets
			Ticket testedTicketBefore = uof.Tickets.Get(TestedTicketId);
			int testedTicketBoughtBefore = testedTicketBefore.BoughtCount;

			// buy a ticket by AfishaSvc
			afisha.BuyTicket(TestedTicketId);

			Assert.AreEqual(testedTicketBoughtBefore + 1, testedTicketBefore.BoughtCount,
				$"(Old ticket count + 1) != (New ticket count)!");
		}

		[TestMethod]
		public void TestAddNullGenrePlay() {
			// get plays from afisha
			var plays = afisha.GetAfishaPlays(null).ToList();
			
			var newPlay = new PlayDTO {
				Genre = null,
				DateTime = DateTime.Now,
				TicketDTOs = new List<TicketDTO>()
			};

			plays.Add(newPlay);

			// save plays with null play inside
			Assert.ThrowsException<ArgumentException>(() => afisha.UpdateAfishaPlays(plays)
				, "Must be exception on inserting play with null Genre");
		}

		[TestMethod]
		public void TestAddNullAuthorPlay() {
			// get plays from afisha
			var plays = afisha.GetAfishaPlays(null).ToList();

			var newPlay = new PlayDTO {
				Author = null
			};

			plays.Add(newPlay);

			// save plays with null play inside
			Assert.ThrowsException<ArgumentException>(() => afisha.UpdateAfishaPlays(plays)
				, "Must be exception on inserting play with null Author");
		}

		[TestMethod]
		public void TestAddOrUpdatePlay() {
			// get plays from afisha
			var plays = afisha.GetAfishaPlays(null).ToList();

			var currentDayTime = DateTime.Now;

			plays.Add(new PlayDTO {
				Name = "TEST_AAA",
				Author = "TEST_BBB",
				Genre = "TEST_GGG",
				DateTime = currentDayTime,
				TicketDTOs = new List<TicketDTO>()
			});

			// save plays
			afisha.UpdateAfishaPlays(plays);

			// get from Repositories actual last play and check
			var lastPlayInDb = uof.Plays.GetAll().First();
			Assert.AreEqual("TEST_AAA", lastPlayInDb.Name);
			Assert.AreEqual("TEST_BBB", lastPlayInDb.Author);
			Assert.AreEqual("TEST_GGG", lastPlayInDb.Genre);
			Assert.AreEqual(currentDayTime, lastPlayInDb.DateTime);
			Assert.AreEqual(0, lastPlayInDb.Tickets.Count);
		}
	}
}
