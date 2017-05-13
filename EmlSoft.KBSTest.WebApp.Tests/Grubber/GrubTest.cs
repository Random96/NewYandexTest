using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.WebApp.Tests.Grubber
{
    [TestClass]
    public class GrubTest
    {
        [TestMethod]
        public void HttpRead()
        {
            Domain.HttpGrubber gruber = new Domain.HttpGrubber();

            string ret = gruber.Grub("http://www.e1.ru");
        }

        [TestMethod]
        public void Search()
        {
            var Container = Util.AutofacConfig.ConfigureContainer();

            Domain.ISourceRepository Rep = Container.Resolve<Domain.ISourceRepository>();

            Task<IEnumerable<Domain.ISearchResult>> ret = Rep.SerachAsync("ОТКАЗ");

            ret.Wait();
        }

        [TestMethod]
        public void TagGrubber()
        {
            string input = "bla bla bla <a bla>qq > rr </a> qq < rr <b> eeee <h1>";

            string output = EmlSoft.KBSTest.Domain.HttpGrubber.StripHTML(input);
        }


    }
}
