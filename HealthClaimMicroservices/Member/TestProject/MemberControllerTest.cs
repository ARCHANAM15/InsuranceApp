using AutoFixture;
using Members.Controllers;
using Members.Models;
using Members.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class MemberControllerTest
    {
        private Mock<IMemberRegService> mockmemberRegservice;
        private Fixture fixture;
        MemberController _memberController;
        MemberController memberController;
        Mock<HealthCareDBContext> mockmemberDBcontext;
        Mock<IConfiguration> mockConfi;

        public MemberControllerTest()
        {
            fixture = new Fixture();
            mockmemberRegservice = new Mock<IMemberRegService>();
            mockmemberDBcontext = new Mock<HealthCareDBContext>();
            mockConfi = new Mock<IConfiguration>();
            IMemberRegService memberRegService = new MemberServiceImp(mockmemberDBcontext.Object);
            _memberController = new MemberController(memberRegService);
            memberController = new MemberController(mockmemberRegservice.Object);


        }
        [TestMethod]
        public void Test_getAllPolicyStatus()
        {
            dynamic result = memberController.GetAllPolicyStatus();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Test_getsaveMember()
        {
            MemberRegTbl mtble = new MemberRegTbl();
            mtble.CreateDate = DateTime.Now;
            mtble.ModifiedDate = DateTime.Now;
            dynamic result = memberController.SaveMember(mtble);
            Assert.AreEqual(200, result.StatusCode);

        }
        [TestMethod]
        public void Test_getsearchMember()
        {
            memberDataList mtble = new memberDataList();
            mtble.FirstName = "test";
            dynamic result = memberController.searchmember(mtble);
            Assert.IsNotNull(result);

        }
    }

 }

