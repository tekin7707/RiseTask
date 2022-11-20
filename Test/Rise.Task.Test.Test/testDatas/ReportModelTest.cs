using Rise.Task.Contact.Domain.Aggregate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Test.Test.testDatas
{
    public class AddReportModelTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {

            yield return new object[] {
                new ReportModel {
                    CreatedDate=new DateTime(2018,1,1),
                    IsReady=false
                }, 200 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
